using Shopper.Domain.Services;
using System.Net.Http.Json;

namespace Shopper.BlazorWebAssembly.Services
{
    public class ColorService : IColorService
    {
        private readonly HttpClient client;

        public ColorService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            return await client.GetFromJsonAsync<IEnumerable<string>>("api/products/colors");
        }
    }
}
