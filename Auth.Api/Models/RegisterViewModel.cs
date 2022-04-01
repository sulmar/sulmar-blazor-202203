using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
