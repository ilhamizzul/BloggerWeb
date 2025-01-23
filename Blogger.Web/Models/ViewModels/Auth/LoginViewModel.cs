using System.ComponentModel.DataAnnotations;

namespace Blogger.Web.Models.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
        public required string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
