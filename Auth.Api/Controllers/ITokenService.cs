using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Controllers
{
    public interface ITokenService
    {
        SecurityToken Create(ApplicationUser model);
    }
}
