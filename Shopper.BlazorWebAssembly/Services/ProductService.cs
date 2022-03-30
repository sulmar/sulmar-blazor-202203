using Shopper.Domain.Models;
using Shopper.Domain.Services;
using System.Net.Http.Json;

namespace Shopper.BlazorWebAssembly.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;
        private readonly ILogger<ProductService> logger;

        public ProductService(HttpClient client, ILogger<ProductService> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAsync(CancellationToken token = default)
        {
            logger.LogInformation("GetAsync");

            await Task.Delay(TimeSpan.FromSeconds(2));

            return await client.GetFromJsonAsync<IEnumerable<Product>>("api/products", token);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task<int> GetCount()
        {
            return await client.GetFromJsonAsync<int>("api/products/count");
        }
    }
}
