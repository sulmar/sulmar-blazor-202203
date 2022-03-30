using Shopper.Domain.Models;

namespace Shopper.Domain.Repositories;

// Szablon 
public interface IEntityRepository<TEntity> 
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCount();
}

