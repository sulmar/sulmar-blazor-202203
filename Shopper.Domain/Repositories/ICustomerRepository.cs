using Shopper.Domain.Models;

namespace Shopper.Domain.Repositories;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    Task<IEnumerable<Customer>> GetByName(string name);
}

