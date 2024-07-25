using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents an item in an order in the order management system.
    /// </summary>
    public class OrderItem : BaseModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the associated order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with this order item.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product associated with this order item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with this order item.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product at the time of the order.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the order item.
        /// </summary>
        public decimal Discount { get; set; }
    }
}
