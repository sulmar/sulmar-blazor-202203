using Bogus;
using Microsoft.AspNetCore.Http.Json;
using Shopper.Api.Hubs;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();
builder.Services.AddSingleton<Faker<Product>, ProductFaker>();

builder.Services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
builder.Services.AddSingleton<Faker<Customer>, CustomerFaker>();

builder.Services.AddSingleton<IColorRepository, FakeColorRepository>();

builder.Services.AddEndpointsApiExplorer();

// Install-Package Swashbuckle.AspNetCore - Version 6.2.3
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(policy =>
//{
//    policy.AddDefaultPolicy(options => options
//        // .WithOrigins("https://localhost:7228")
//        .AllowAnyOrigin()            
//        .AllowAnyMethod()
//        .AllowAnyHeader());

// builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddCors(
    policy => policy.AddDefaultPolicy(
    options => options.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
    .AllowAnyHeader()
    .AllowAnyMethod()));

builder.Services.AddSignalR();

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

// GET api/products/count
app.MapGet("api/products/count",  async (IProductRepository productRepository) => await productRepository.GetCount());

// PUT api/products/{id}
app.MapPut("api/products/{id:int}", async (IProductRepository productRepository, Product product) => await productRepository.UpdateAsync(product));

// PATCH api/products/{id}


// GET api/customers
app.MapGet("api/customers", async (ICustomerRepository customerRepository) => await customerRepository.GetAsync());

// GET api/customers/count
app.MapGet("api/customers/count", async (ICustomerRepository customerRepository) => await customerRepository.GetCount());

// GET api/customers/{id}
app.MapGet("api/customers/{id:int}", async (ICustomerRepository customerRepository, int id) => await customerRepository.GetByIdAsync(id));

// GET api/customers/search?name="John"
app.MapGet("api/customers/search", async (ICustomerRepository customerRepository, string name) => await customerRepository.GetByName(name));

// GET api/products/colors
app.MapGet("api/products/colors", async (IColorRepository colorRepository) => await colorRepository.Get());

app.MapHub<TimerHub>("ws/current-time");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
