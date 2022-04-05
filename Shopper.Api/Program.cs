using Bogus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.ML;
using Microsoft.IdentityModel.Tokens;
using Shopper.Api.Hubs;
using Shopper.Api.Models;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;
using System.Text;
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


builder.Services.AddAuthorization();

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        string secretKey = "your-256-bit-secret";

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = "ABC",
            ValidateAudience = true,
            ValidIssuer = "ABC",
            ValidateIssuer = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuerSigningKey = true
        };

        // optionalne: odczyt tokena z ciasteczka
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["X-Access-Token"];

                return Task.CompletedTask;

            }
        };
    })
    ;


builder.Services.AddPredictionEnginePool<SentimentInput, SentimentPrediction>()
    .FromFile("text-sentiment-model.zip", watchForChanges: true);

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
app.MapPut("api/products/{id:int}", async (IProductRepository productRepository, 
    IHubContext<StrongTypedProductsHub, IProductClient> productHub, Product product) =>
{
    await productRepository.UpdateAsync(product);

    // await productHub.Clients.All.SendAsync("ProductChanged", product);

    // await productHub.Clients.All.ProductChanged(product);

    await productHub.Clients.Group(product.Color).ProductChanged(product);

});

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

app.MapPost("/upload", async (HttpRequest request) =>
{
    if (!request.HasFormContentType)
        return Results.BadRequest();

    var form = await request.ReadFormAsync();
    var formFile = form.Files["file"];

    if (formFile is null || formFile.Length == 0)
        return Results.BadRequest();

    await using var stream = formFile.OpenReadStream();

    var reader = new StreamReader(stream);
    var text = await reader.ReadToEndAsync();

    return Results.Ok(text);
}).Accepts<IFormFile>("multipart/form-data");


app.MapPost("api/sentiment", 
    (PredictionEnginePool<SentimentInput, SentimentPrediction> predictionEnginePool, [FromBody] string text) =>
{
    var sentimentInput = new SentimentInput { Text = text };

    var prediction = predictionEnginePool.Predict(sentimentInput);

    return prediction;
});

app.MapHub<TimerHub>("ws/current-time");
app.MapHub<StrongTypedProductsHub>("ws/products");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();


app.Run();
