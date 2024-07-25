
using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using OrderSystem.Core.Models.Emailllll;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public OrderService(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Creates a new order and performs necessary operations.
        /// </summary>
        /// <param name="order">The order to create.</param>
        public async Task CreateOrderAsync(Order order)
        {
            await ValidateStockAsync(order);
            ApplyTieredDiscounts(order);
            await SetCustomerAsync(order);

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            await GenerateInvoiceAsync(order);
            await SendOrderConfirmationEmailAsync(order.Customer.Email);
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <param name="status">The new status of the order.</param>
        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found");

            order.Status = status;
            await _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();

            await SendOrderStatusUpdateEmailAsync(order);
        }


        private async Task ValidateStockAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product: {product?.Name ?? "Unknown"}");
                }
                product.Stock -= item.Quantity;
                await _unitOfWork.Repository<Product>().Update(product);
            }
        }

        private void ApplyTieredDiscounts(Order order)
        {
            decimal discount = order.TotalAmount switch
            {
                > 200 => 0.1m,
                > 100 => 0.05m,
                _ => 0m
            };

            order.TotalAmount -= order.TotalAmount * discount;
        }

        private async Task SetCustomerAsync(Order order)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(order.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }
            order.Customer = customer;
        }

        private async Task GenerateInvoiceAsync(Order order)
        {
            var invoice = new Invoice
            {
                OrderId = order.Id,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount
            };

            await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
            await _unitOfWork.CompleteAsync();
        }

        private async Task SendOrderConfirmationEmailAsync(string customerEmail)
        {
            var emailMessage = new EmailMessage
            {
                To = customerEmail,
                Subject = "Order Confirmation",
                Body = "Your order has been placed successfully."
            };
             _emailSender.SendEmail(emailMessage);
        }

        private async Task SendOrderStatusUpdateEmailAsync(Order order)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(order.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            var emailMessage = new EmailMessage
            {
                To = customer.Email,
                Subject = "Your order status has changed",
                Body = $"Your order ID {order.Id} status has been updated to {order.Status}."
            };
             _emailSender.SendEmail(emailMessage);
        }
    }


}
