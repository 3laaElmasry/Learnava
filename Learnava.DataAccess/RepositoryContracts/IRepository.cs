

using System.Linq.Expressions;

namespace Learnava.DataAccess.RepositoryContracts
{
    public interface IRepository<T> where T : class
    {
        // Get all entities asynchronously
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        // Get a single entity asynchronously with filtering
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

        // Add an entity asynchronously
        Task AddAsync(T entity); 

        // Remove an entity
        void Remove(T entity);

        // Remove multiple entities
        void RemoveRange(IEnumerable<T> entities);

        // Update Entity
        void Update(T entity);

        Task Save();
    }
}
