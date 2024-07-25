using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Repositories
{
    /// <summary>
    /// Implements the Unit of Work pattern for managing repositories and transactions.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;
        private Hashtable _repositories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(OrderManagementDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        /// <summary>
        /// Saves changes to the database asynchronously.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        /// <summary>
        /// Disposes the context asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous dispose operation.</returns>
        public ValueTask DisposeAsync() => _context.DisposeAsync();

        /// <summary>
        /// Retrieves a repository for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/> for the specified entity type.</returns>
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
