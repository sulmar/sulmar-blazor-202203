using Shopper.Domain.Models;
using Shopper.Domain.Services;
using System.Net.Http.Json;

namespace Shopper.BlazorWebAssembly.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient client;

        public CustomerService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Customer>> GetAsync(CancellationToken token = default)
        {
            return await client.GetFromJsonAsync<IEnumerable<Customer>>("api/customers", token);
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await client.GetFromJsonAsync<int>("api/customers/count");
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
