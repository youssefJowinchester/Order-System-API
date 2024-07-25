using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods for managing orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateOrderAsync(Order order);

        /// <summary>
        /// Updates the status of an existing order asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <param name="status">The new status for the order.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }

}
