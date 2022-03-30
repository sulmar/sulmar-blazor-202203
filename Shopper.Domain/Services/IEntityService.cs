using Shopper.Domain.Models;

namespace Shopper.Domain.Services
{
    public interface IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);
        Task<TEntity> GetByIdAsync(int id);
        Task<int> GetCount();
        Task UpdateAsync(TEntity entity);
    }
}
