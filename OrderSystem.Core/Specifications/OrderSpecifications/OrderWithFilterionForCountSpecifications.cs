using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.OrderSpecifications
{
    /// <summary>
    /// Specifies criteria for counting orders with optional filtering.
    /// </summary>
    public class OrderWithFilteringForCountSpecifications : BaseSpecifications<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderWithFilteringForCountSpecifications"/> class.
        /// </summary>
        public OrderWithFilteringForCountSpecifications() : base() { }
    }

}
