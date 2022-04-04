using Shopper.Domain.Models;
using Shopper.Domain.Services;
using System.Net.Http.Json;

namespace Shopper.BlazorWebAssembly.Services
{
    public class SentimentService : ISentimentService
    {
        private readonly HttpClient client;

        public SentimentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Sentiment> CalculateAsync(string text)
        {
            var response = await client.PostAsJsonAsync($"api/sentiment", text);

            var content = await response.Content.ReadAsStringAsync();

            var result = await response.Content.ReadFromJsonAsync<Sentiment>();

            return result;
            
        }
    }
}
