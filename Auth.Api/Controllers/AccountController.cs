using Auth.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var result = await authService.IsValidAsync(model.Login, model.Password);

                if (result.isValid)
                {
                    var securityToken = tokenService.Create(result.user);

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
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] IPasswordHasher<ApplicationUser> passwordHasher,            
            [FromBody] RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Account = model.Account  };

            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, model.Email));

            var roleExists = await roleManager.RoleExistsAsync("developer");

            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("developer"));
            }

            await userManager.AddToRoleAsync(user, "developer");

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result);
    
        }
    }
}
