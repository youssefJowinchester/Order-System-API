using Microsoft.IdentityModel.Tokens;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.CustomerSpecifications
{
    /// <summary>
    /// Specifies criteria for querying orders associated with a specific customer, including filtering and sorting options.
    /// </summary>
    public class CustomerWithOrdersSpecifications : BaseSpecifications<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerWithOrdersSpecifications"/> class with specified customer ID and sorting options.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="orderSpec">The specifications for querying orders.</param>
        public CustomerWithOrdersSpecifications(int customerId, CustomerSpecParams orderSpec)
            : base(p => p.CustomerId == customerId)
        {
            Includes.Add(e => e.Customer);
            Includes.Add(e => e.OrderItems);

            if (!string.IsNullOrEmpty(orderSpec.sort))
            {
                switch (orderSpec.sort)
                {
                    case "Date":
                        AddOrderBy(p => p.OrderDate);
                        break;
                    case "DateDesc":
                        AddOrderByDesc(p => p.OrderDate);
                        break;
                    default:
                        AddOrderBy(p => p.OrderDate);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.OrderDate);
            }

            ApplyPagination(orderSpec.PageSize * (orderSpec.PageIndex - 1), orderSpec.PageSize);
        }
    }

}
