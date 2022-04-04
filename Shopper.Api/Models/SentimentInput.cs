using Microsoft.ML.Data;

namespace Shopper.Api.Models
{
    public class SentimentInput
    {
        [LoadColumn(0)]
        public bool Label { get; set; }

        [LoadColumn(1)]
        public string Text { get; set; }
    }
}
