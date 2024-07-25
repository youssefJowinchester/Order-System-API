using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications.ProductSpecifications
{
    /// <summary>
    /// Specifies criteria for counting products with optional filtering.
    /// </summary>
    public class ProductWithFilterionForCountSpecifications : BaseSpecifications<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWithFilterionForCountSpecifications"/> class.
        /// </summary>
        public ProductWithFilterionForCountSpecifications() : base() { }
    }

}
