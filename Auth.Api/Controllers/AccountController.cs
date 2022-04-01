using Auth.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Api.Controllers
{


    public class AccountController : ControllerBase
    {
        // POST api/token 
        // json: {login,password}        
        [AllowAnonymous]
        [HttpPost("api/token")]
        public async Task<ActionResult<string>> CreateTokenAsync(
            [FromServices] ITokenService tokenService,
            [FromServices] IAuthService authService,
            [FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = await authService.IsValidAsync(model.Login, model.Password);

                if (isValid)
                {
                    var securityToken = tokenService.Create(model);

                    var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                    return Ok(token);
                }

            }

            return Unauthorized();

        }

        [AllowAnonymous]
        [HttpPost("api/users")]
        public async Task<ActionResult> CreateUser(
            [FromServices] UserManager<ApplicationUser> userManager, 
            [FromServices] IPasswordHasher<ApplicationUser> passwordHasher,            
            [FromBody] RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Account = model.Account  };

            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result);
    
        }
    }
}
