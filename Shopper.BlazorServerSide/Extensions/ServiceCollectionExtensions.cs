using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;

namespace Shopper.BlazorServerSide.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeServices(this IServiceCollection services)
    {
        AddFakeProductsServices(services);        

        return services;
    }

    public static void AddFakeProductsServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, FakeProductRepository>();
        services.AddSingleton<Faker<Product>, ProductFaker>();
    }
}
