using Blazored.Toast;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Shopper.BlazorWebAssembly;
using Shopper.BlazorWebAssembly.Services;
using Shopper.Domain.Models;
using Shopper.Domain.Services;
using Shopper.Domain.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUri = new Uri("http://localhost:5000");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress =  baseUri });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IColorService, ColorService>();

// dotnet add package Microsoft.AspNetCore.SignalR.Client
//builder.Services.AddSingleton<HubConnection>(options =>
//{
//    return new HubConnectionBuilder()
//        .WithUrl($"{baseUri}ws/current-time")
//        .WithAutomaticReconnect()
//        .Build();
//});

builder.Services.AddSingleton<HubConnection>(options =>
{
    return new HubConnectionBuilder()
        .WithUrl($"{baseUri}ws/products")
        .WithAutomaticReconnect()
        .Build();
});


// dotnet add package Blazored.Toast
builder.Services.AddBlazoredToast();


builder.Services
    .AddSingleton<IJSInProcessRuntime>(sp=>(IJSInProcessRuntime)sp.GetRequiredService<IJSRuntime>());


builder.Services.AddSingleton<StateContainer>();
builder.Services.AddSingleton<IStorageProvider, LocalStorageProvider>();

builder.Services.AddTransient<IValidator<Product>, ProductValidator>();


await builder.Build().RunAsync();
