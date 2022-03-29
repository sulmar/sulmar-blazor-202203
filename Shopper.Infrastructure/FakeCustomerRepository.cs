using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;

namespace Shopper.Infrastructure
{
    public class FakeCustomerRepository : FakeEntityRepository<Customer>, ICustomerRepository
    {
        public FakeCustomerRepository(Faker<Customer> faker) : base(faker)
        {
        }

        public Task<IEnumerable<Customer>> GetByName(string name)
        {
            var customers = _entities.Values
                .Where(p=>p.FirstName.Contains(name) || p.LastName.Contains(name))
                .ToList();

            return Task.FromResult<IEnumerable<Customer>>(customers);
        }

        public override async Task RemoveAsync(int id)
        {
            var customer = await GetByIdAsync(id);

            customer.IsRemoved = true;            
        }
    }
}