using Microsoft.AspNetCore.SignalR;

namespace Shopper.Api.Hubs
{
    public class TimerHub : Hub
    {
        private readonly ILogger<TimerHub> _logger;

        public override Task OnConnectedAsync()
        {
            // zła praktyka
            // _logger.LogInformation($"Connected ConnectionId: {this.Context.ConnectionId}");

            // dobra praktyka
            _logger.LogInformation("Connected ConnectionId: {0}", this.Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.UtcNow;

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
