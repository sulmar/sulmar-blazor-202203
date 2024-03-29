﻿using Microsoft.AspNetCore.SignalR;
using Shopper.Domain.Models;

namespace Shopper.Api.Hubs
{
    public class ProductHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
            
        }
    }

    public class StrongTypedProductsHub : Hub<IProductClient>
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task Subscribe(string color)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, color);
        }
    }

    public interface IProductClient
    {
        Task ProductChanged(Product product);
        Task ProductRemoved(Product product);
        
    }
}
