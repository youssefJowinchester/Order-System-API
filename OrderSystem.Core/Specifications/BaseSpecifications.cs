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
    /// Base class for specifications to be used in querying entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseModel
    {
        /// <summary>
        /// Gets or sets the criteria for filtering the entities.
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of related entities to include in the query.
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Gets or sets the expression for ordering the results in ascending order.
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; set; } = null;

        /// <summary>
        /// Gets or sets the expression for ordering the results in descending order.
        /// </summary>
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        /// <summary>
        /// Gets or sets the number of records to skip for pagination.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of records to take for pagination.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pagination is enabled.
        /// </summary>
        public bool IsPaginationEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecifications{T}"/> class.
        /// </summary>
        public BaseSpecifications() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecifications{T}"/> class with a specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria to filter entities.</param>
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria; // e.g., P => P.Id == 10 
        }

        /// <summary>
        /// Adds an order by expression for ascending ordering.
        /// </summary>
        /// <param name="orderByExpression">The expression to order by.</param>
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Adds an order by expression for descending ordering.
        /// </summary>
        /// <param name="orderByDescExpression">The expression to order by in descending order.</param>
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        /// <summary>
        /// Applies pagination settings.
        /// </summary>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="take">The number of records to take.</param>
        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }
    }

}
