using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;

namespace Shopper.Infrastructure
{
    public class FakeProductRepository : IProductRepository
    {
        private IDictionary<int, Product> _products;

        public FakeProductRepository(Faker<Product> faker)
        {
            var list = faker.Generate(10_000);

            _products = list.ToDictionary(p => p.Id);
        }


        public Task AddAsync(Product product)
        {
            int id = _products.Values.Max(p=>p.Id);

            product.Id = ++id;

            _products.Add(product.Id, product);

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(_products.ContainsKey(id));
        }

        public Task<IEnumerable<Product>> GetAsync()
        {
            return Task.FromResult(_products.Values.AsEnumerable());
        }

        public Task<IEnumerable<Product>> GetByColor(string color)
        {
            var list = _products.Values
                .Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(list);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            if (_products.TryGetValue(id, out var product))
            {
                return Task.FromResult(product);
            }
            else
            {
                return Task.FromResult<Product>(null);
            }
        }

        public Task RemoveAsync(int id)
        {
            _products.Remove(id);
            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            await RemoveAsync(product.Id);
            _products.Add(product.Id, product);
        }
    }
}