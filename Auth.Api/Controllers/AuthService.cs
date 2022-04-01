using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Controllers
{


    // dotnet add package Microsoft.AspNetCore.Identity

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> IsValidAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            var isValid = await userManager.CheckPasswordAsync(user, password);

            return isValid;
        }
    }
}
