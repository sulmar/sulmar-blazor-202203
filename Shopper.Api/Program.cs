using Bogus;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();
builder.Services.AddSingleton<Faker<Product>, ProductFaker>();


//builder.Services.AddCors(policy =>
//{
//    policy.AddDefaultPolicy(options => options
//        // .WithOrigins("https://localhost:7228")
//        .AllowAnyOrigin()            
//        .AllowAnyMethod()
//        .AllowAnyHeader());

builder.Services.AddCors(
    policy => policy.AddDefaultPolicy(
    options => options.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

// Minimal API (od .NET 6)

// GET api/products
app.MapGet("api/products", async 
    (IProductRepository productRepository) => await productRepository.GetAsync());

// GET api/products/{id}
app.MapGet("api/products/{id:int}", async
    (IProductRepository productRepository, int id) => await productRepository.GetByIdAsync(id));

app.Run();
