using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.CustomerSpecifications
{
    /// <summary>
    /// Specifies criteria for counting orders associated with a specific customer, including optional filtering.
    /// </summary>
    public class OrdersWithCountSpecifications : BaseSpecifications<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersWithCountSpecifications"/> class for a specific customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="specParams">The specifications for filtering orders.</param>
        public OrdersWithCountSpecifications(int customerId, CustomerSpecParams specParams)
            : base(n => n.CustomerId == customerId)
        {
            if (!string.IsNullOrEmpty(specParams.Search))
            {
                Criteria = p => p.CustomerId == customerId; // Additional filtering can be added here if needed
            }
        }
    }

}
