using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Shopper.BlazorWebAssembly
{

    // dotnet add package Microsoft.AspNetCore.Components.WebAssembly.Authentication 
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {
        }


        // https://docs.microsoft.com/pl-pl/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-6.0
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
