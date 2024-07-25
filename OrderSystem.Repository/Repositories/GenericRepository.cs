using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Specifications;
using OrderSystem.Repository.Data;
using OrderSystem.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Repositories
{
    /// <summary>
    /// A generic repository for managing entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the entity that the repository manages, constrained to <see cref="BaseModel"/>.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly OrderManagementDbContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(OrderManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        #region All Methods

        /// <summary>
        /// Adds an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public async Task Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        /// <summary>
        /// Gets an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        #endregion

        #region Get Data By Spec

        /// <summary>
        /// Gets all entities that match the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to apply.</param>
        /// <returns>A list of entities that match the criteria.</returns>
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        /// <summary>
        /// Gets a single entity that matches the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to apply.</param>
        /// <returns>The entity that matches the criteria, or null if not found.</returns>
        public async Task<T?> GetwithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the count of entities that match the specified criteria asynchronously.
        /// </summary>
        /// <param name="spec">The specifications to apply.</param>
        /// <returns>The count of entities that match the criteria.</returns>
        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        #endregion
    }
}
