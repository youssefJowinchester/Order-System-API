using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Repositories.Interfaces
{
    /// <summary>
    /// Represents a unit of work that allows to group multiple operations into a single transaction.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Gets the generic repository for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/>.</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;

        /// <summary>
        /// Commits all the changes made in the current transaction asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> CompleteAsync();
    }

}
