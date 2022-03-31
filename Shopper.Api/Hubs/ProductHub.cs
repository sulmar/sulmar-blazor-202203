using Microsoft.AspNetCore.SignalR;

namespace Shopper.Api.Hubs
{
    public class ProductHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
            
        }
    }
}
