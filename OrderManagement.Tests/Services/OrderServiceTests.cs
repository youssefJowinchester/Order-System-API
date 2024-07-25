using Moq;
using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using OrderSystem.Core.Models.Emailllll;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Services.Interfaces;
using OrderSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmailSender> _mockEmailSender;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmailSender = new Mock<IEmailSender>();
            _orderService = new OrderService(_mockUnitOfWork.Object, _mockEmailSender.Object);
        }





        [Fact]
        public async Task CreateOrderAsync_InsufficientStock_ThrowsException()
        {
            // Arrange
            var order = new Order
            {
                CustomerId = 1,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 5 }
            }
            };

            _mockUnitOfWork.Setup(repo => repo.Repository<Product>().GetByIdAsync(1))
                .ReturnsAsync(new Product { Id = 1, Name = "Test Product", Stock = 3 });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync(order));
            Assert.Equal("Insufficient stock for product: Test Product", exception.Message);
        }



        [Fact]
        public async Task CreateOrderAsync_ValidOrder_SavesOrderAndGeneratesInvoice()
        {
            // Arrange
            var order = new Order
            {
                CustomerId = 1,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2 }
            }
            };

            _mockUnitOfWork.Setup(repo => repo.Repository<Product>().GetByIdAsync(1))
                .ReturnsAsync(new Product { Id = 1, Name = "Test Product", Stock = 10 });
            _mockUnitOfWork.Setup(repo => repo.Repository<Customer>().GetByIdAsync(1))
                .ReturnsAsync(new Customer { Id = 1, Email = "customer@test.com" });

            // Act
            await _orderService.CreateOrderAsync(order);

            // Assert
            _mockUnitOfWork.Verify(repo => repo.Repository<Order>().AddAsync(It.IsAny<Order>()), Times.Once);
            _mockUnitOfWork.Verify(repo => repo.Repository<Invoice>().AddAsync(It.IsAny<Invoice>()), Times.Once);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<EmailMessage>()), Times.Once);
        }



        [Fact]
        public async Task UpdateOrderStatusAsync_ValidOrder_UpdatesStatusAndSendsEmail()
        {
            // Arrange
            var order = new Order { Id = 1, CustomerId = 1, Status = OrderStatus.Pending };
            _mockUnitOfWork.Setup(repo => repo.Repository<Order>().GetByIdAsync(1)).ReturnsAsync(order);
            _mockUnitOfWork.Setup(repo => repo.Repository<Customer>().GetByIdAsync(1))
                .ReturnsAsync(new Customer { Id = 1, Email = "customer@test.com" });

            // Act
            await _orderService.UpdateOrderStatusAsync(1, OrderStatus.Shipped);

            // Assert
            Assert.Equal(OrderStatus.Shipped, order.Status);
            _mockUnitOfWork.Verify(repo => repo.Repository<Order>().Update(order), Times.Once);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<EmailMessage>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_OrderNotFound_ThrowsException()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.Repository<Order>().GetByIdAsync(1)).ReturnsAsync((Order)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.UpdateOrderStatusAsync(1, OrderStatus.Shipped));
            Assert.Equal("Order not found", exception.Message);
        }


    }
}
