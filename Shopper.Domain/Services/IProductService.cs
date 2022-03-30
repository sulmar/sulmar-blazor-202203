using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain.Services
{
    public interface IProductService : IEntityService<Product>
    {
        
    }

    public interface IColorService
    {
        Task<IEnumerable<string>> GetAsync();
    }

}
