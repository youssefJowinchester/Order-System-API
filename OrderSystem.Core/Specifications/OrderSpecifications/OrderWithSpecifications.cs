using Microsoft.IdentityModel.Tokens;
using OrderSystem.Core.Models;
using OrderSystem.Core.Specifications.ProductSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.OrderSpecifications
{
    /// <summary>
    /// Specifies criteria for querying orders, including filtering and sorting options.
    /// </summary>
    public class OrderWithSpecifications : BaseSpecifications<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderWithSpecifications"/> class with specified sorting and includes.
        /// </summary>
        /// <param name="orderSpec">The specifications for querying orders.</param>
        public OrderWithSpecifications(OrderSpecParams orderSpec)
        {
            Includes.Add(o => o.Customer);
            Includes.Add(o => o.OrderItems);

            if (!string.IsNullOrEmpty(orderSpec.sort))
            {
                switch (orderSpec.sort)
                {
                    case "DateAsc":
                        AddOrderBy(o => o.OrderDate);
                        break;

                    case "DateDesc":
                        AddOrderByDesc(o => o.OrderDate);
                        break;

                    default:
                        AddOrderBy(o => o.OrderDate);
                        break;
                }
            }
            else
            {
                AddOrderBy(o => o.OrderDate);
            }

            ApplyPagination(orderSpec.PageSize * (orderSpec.PageIndex - 1), orderSpec.PageSize);
        }

        public OrderWithSpecifications(int id ,OrderSpecParams orderSpec) : base(o => o.Id == id)
        {
            Includes.Add(o => o.Customer);
            Includes.Add(o => o.OrderItems);

            if (!string.IsNullOrEmpty(orderSpec.sort))
            {
                switch (orderSpec.sort)
                {
                    case "DateAsc":
                        AddOrderBy(o => o.OrderDate);
                        break;

                    case "DateDesc":
                        AddOrderByDesc(o => o.OrderDate);
                        break;

                    default:
                        AddOrderBy(o => o.OrderDate);
                        break;
                }
            }
            else
            {
                AddOrderBy(o => o.OrderDate);
            }

            ApplyPagination(orderSpec.PageSize * (orderSpec.PageIndex - 1), orderSpec.PageSize);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderWithSpecifications"/> class for a specific order by ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        public OrderWithSpecifications(int id) : base(o => o.Id == id)
        {
            Includes.Add(o => o.Customer);
            Includes.Add(o => o.OrderItems);
        }
    }

}
