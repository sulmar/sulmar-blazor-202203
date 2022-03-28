using Shopper.Domain.Models;
using Shopper.Domain.Services;
using System.Net.Http.Json;

namespace Shopper.BlazorWebAssembly.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;

        public ProductService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Product>> GetAsync(CancellationToken token = default)
        {
            return await client.GetFromJsonAsync<IEnumerable<Product>>("api/products", token);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<Product>($"api/products/{id}");
        }
    }
}
