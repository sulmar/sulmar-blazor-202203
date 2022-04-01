using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
