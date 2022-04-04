using Microsoft.ML.Data;

namespace Shopper.Api.Models
{
    public class SentimentPrediction : SentimentInput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
