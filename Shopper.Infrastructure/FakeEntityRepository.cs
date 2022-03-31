using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;

namespace Shopper.Infrastructure
{
    public abstract class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected IDictionary<int, TEntity> _entities;

        public FakeEntityRepository(Faker<TEntity> faker)
        {
            _entities = faker.Generate(1000).ToDictionary(p => p.Id);    
        }

        public virtual Task AddAsync(TEntity entity)
        {
            int id = _entities.Values.Max(p => p.Id);

            entity.Id = ++id;

            _entities.Add(entity.Id, entity);

            return Task.CompletedTask;
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(_entities.ContainsKey(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetAsync()
        {
            return Task.FromResult(_entities.Values.AsEnumerable());
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            if (_entities.TryGetValue(id, out var entity))
            {
                return Task.FromResult(entity);
            }
            else
            {
                return Task.FromResult<TEntity>(null);
            }
        }

        public Task<int> GetCount()
        {
            return Task.FromResult(_entities.Count);
        }

        public virtual Task RemoveAsync(int id)
        {
            _entities.Remove(id);
            return Task.CompletedTask;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {                      
            await RemoveAsync(entity.Id);
            _entities.Add(entity.Id, entity);
        }
    }
}