using Bogus;
using Shopper.Domain.Models;

namespace Shopper.Infrastructure.Fakers;

// dotnet add package Bogus
public class ProductFaker : Faker<Product>
{
    readonly decimal[] discounts = new decimal[] { 0.05m, 0.1m, 0.15m, 0.2m };

    public ProductFaker()
    {
        StrictMode(true);   // pilnuje czy wszystkie właściwości modelu są objęte regułą
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
        RuleFor(p => p.Color, f => f.Commerce.Color());
        RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 100)));
        RuleFor(p => p.ImageLink, f => f.Image.PicsumUrl());
        RuleFor(p => p.HasDiscount, f => f.Random.Bool(0.2f));
        RuleFor(p => p.Discount, (f, product) => product.HasDiscount ? f.PickRandom(discounts) : null);
        RuleFor(p => p.Supplier, f => f.Company.CompanyName());
        RuleFor(p => p.Size, f => f.PickRandom<SizeType>());
        RuleFor(p => p.Tags, f => f.Commerce.Categories(f.Random.Int(1, 3)));
    }    
}

