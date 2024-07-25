using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Models;
using OrderSystem.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Specifications
{
    /// <summary>
    /// Evaluates specifications for entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that the specifications apply to, constrained to <see cref="BaseModel"/>.</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseModel
    {
        /// <summary>
        /// Applies the specified criteria and includes to the input query.
        /// </summary>
        /// <param name="inputQuery">The initial query to which specifications will be applied.</param>
        /// <param name="spec">The specifications containing filtering, ordering, and inclusion criteria.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> with the applied specifications.</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            // Apply filtering criteria
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            // Apply ordering
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            // Apply pagination if enabled
            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            // Apply includes for eager loading
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
