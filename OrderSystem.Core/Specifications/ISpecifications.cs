using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specifications
{
    /// <summary>
    /// Represents the specifications pattern for querying entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface ISpecifications<T> where T : BaseModel
    {
        /// <summary>
        /// Gets or sets the criteria for filtering the entities.
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; set; }

        /// <summary>
        /// Gets or sets the list of related entities to include in the query.
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; set; }

        /// <summary>
        /// Gets or sets the expression for ordering the results in ascending order.
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the expression for ordering the results in descending order.
        /// </summary>
        Expression<Func<T, object>> OrderByDesc { get; set; }

        /// <summary>
        /// Gets or sets the number of records to skip for pagination.
        /// </summary>
        int Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of records to take for pagination.
        /// </summary>
        int Take { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pagination is enabled.
        /// </summary>
        bool IsPaginationEnabled { get; set; }
    }


}
