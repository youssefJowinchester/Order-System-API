using OrderSystem.Core.Models;
using OrderSystem.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations and specifications pattern.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IGenericRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Update(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Delete(T entity);

        #region Get Data By Spec

        /// <summary>
        /// Gets all entities that match the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to match.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of entities that match the criteria.</returns>
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        /// <summary>
        /// Gets a single entity that matches the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to match.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

        /// <summary>
        /// Gets the count of entities that match the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to match.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities that match the criteria.</returns>
        Task<int> GetCountAsync(ISpecifications<T> spec);

        #endregion
    }


}
