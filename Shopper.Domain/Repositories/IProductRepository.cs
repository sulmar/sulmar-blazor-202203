using Shopper.Domain.Models;

namespace Shopper.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAsync();
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task RemoveAsync(int id);
    Task<bool> ExistsAsync(int id);

    Task<IEnumerable<Product>> GetByColor(string color);
}

