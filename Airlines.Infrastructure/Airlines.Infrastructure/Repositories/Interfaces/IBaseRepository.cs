using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    where T : class
    {
        /// <summary>
        /// Retrieves an entity of type T by its ID.
        /// </summary>
        Task<T?> GetByIdAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all entities of type T.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);

        /// <summary>
        /// Finds entities of type T based on a predicate.
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        /// <summary>
        /// Finds a single entity of type T based on a predicate.
        /// </summary>
        Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        /// <summary>
        /// Finds a single entity of type T based on a predicate without tracking it in the DbContext.
        /// </summary>
        Task<T?> FindOneAsNoTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        /// <summary>
        /// Adds a new entity of type T.
        /// </summary>
        Task<int> AddAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Adds a range of new entities of type T.
        /// </summary>
        Task<int> AddRangeAsync(List<T> entities, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing entity of type T.
        /// </summary>
        Task<int> UpdateAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Updates a range of existing entities of type T.
        /// </summary>
        Task<int> UpdateRangeAsync(List<T> entities, CancellationToken ct = default);

        /// <summary>
        /// Deletes an existing entity of type T.
        /// </summary>
        Task<int> DeleteAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Returns a queryable of all entities of type T.
        /// </summary>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Deletes existing entities of type T.
        /// </summary>
        Task<int> DeleteRangeAsync(List<T> entities, CancellationToken ct = default);

        Task<int> RemoveRangeAsync(IQueryable<T> entities, CancellationToken ct = default);

    }
}
