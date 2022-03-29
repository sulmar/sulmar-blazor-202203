using Shopper.Domain.Models;

namespace Shopper.Domain.Services
{
    public interface ICustomerService : IEntityService<Customer>
    {
        Task<IEnumerable<Customer>> GetByName(string name); 
    }
}
