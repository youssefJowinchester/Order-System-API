using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents an invoice associated with an order in the order management system.
    /// </summary>
    public class Invoice : BaseModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the associated order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with this invoice.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the invoice was created.
        /// </summary>
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets or sets the total amount of the invoice.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
