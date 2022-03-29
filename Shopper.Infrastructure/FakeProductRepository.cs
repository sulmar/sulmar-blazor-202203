using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;

namespace Shopper.Infrastructure
{

    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        public FakeProductRepository(Faker<Product> faker)
            : base(faker)
        {
        }

        public Task<IEnumerable<Product>> GetByColor(string color)
        {
            var products = _entities.Values
               .Where(p => p.Color.Contains(color))
               .ToList();

            return Task.FromResult<IEnumerable<Product>>(products);
        }
    }
}