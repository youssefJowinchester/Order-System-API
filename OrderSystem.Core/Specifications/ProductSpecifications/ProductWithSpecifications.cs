using Microsoft.IdentityModel.Tokens;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.ProductSpecifications
{
    /// <summary>
    /// Specifies criteria for querying products, including filtering and sorting options.
    /// </summary>
    public class ProductWithSpecifications : BaseSpecifications<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWithSpecifications"/> class with specified search and sorting parameters.
        /// </summary>
        /// <param name="productSpec">The specifications for querying products.</param>
        public ProductWithSpecifications(ProductSpecParams productSpec)
            : base(P => string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search.ToLower()))
        {
            if (!string.IsNullOrEmpty(productSpec.sort))
            {
                switch (productSpec.sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            // Calculate pagination
            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWithSpecifications"/> class for a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        public ProductWithSpecifications(int id) : base(P => P.Id == id) { }
    }

}
