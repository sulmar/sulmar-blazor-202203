using Shopper.Domain.Models;

namespace Shopper.Domain.Repositories;

public interface IProductRepository : IEntityRepository<Product>
{
    Task<IEnumerable<Product>> GetByColor(string color);
}

