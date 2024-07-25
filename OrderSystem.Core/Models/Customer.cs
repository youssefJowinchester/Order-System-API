using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents a customer in the order management system.
    /// </summary>
    public class Customer : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the collection of orders associated with the customer.
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
