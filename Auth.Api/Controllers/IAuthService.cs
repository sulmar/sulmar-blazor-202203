namespace Auth.Api.Controllers
{
    public interface IAuthService
    {
        Task<bool> IsValidAsync(string login, string password);
    }
}
