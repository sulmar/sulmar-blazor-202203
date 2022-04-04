using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain.Models
{
    public class Sentiment : Base
    {
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }

}
