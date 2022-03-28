using Bogus;
using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure.Fakers
{
    // dotnet add package Bogus
    public class ProductFaker : Faker<Product>
    {

    }
}
