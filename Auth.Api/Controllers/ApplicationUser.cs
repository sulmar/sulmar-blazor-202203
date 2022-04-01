using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Controllers
{
    public class ApplicationUser : IdentityUser
    {
        public string Account { get; set; }
    }
}
