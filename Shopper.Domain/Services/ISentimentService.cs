using Shopper.Domain.Models;

namespace Shopper.Domain.Services
{
    public interface ISentimentService
    {
        Task<Sentiment> PredictAsync(string text);
    }
}
