using OrderSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents an order in the order management system.
    /// </summary>
    public class Order : BaseModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with this order.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the collection of items included in the order.
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        /// <summary>
        /// Gets or sets the payment method used for the order.
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
