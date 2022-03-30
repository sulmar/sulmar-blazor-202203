using Bogus;
using Shopper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure
{
    public class FakeColorRepository : IColorRepository
    {
        private readonly IEnumerable<string> colors = new List<string>()
        {
            "red",
            "blue",
            "green"
        };

        public Task<IEnumerable<string>> Get()
        {
            return Task.FromResult(colors);
        }
    }
}
