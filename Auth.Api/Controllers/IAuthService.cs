namespace Auth.Api.Controllers
{
    public interface IAuthService
    {
        Task<(bool isValid, ApplicationUser user)> IsValidAsync(string login, string password);
    }
}
