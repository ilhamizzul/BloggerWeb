using System.ComponentModel.DataAnnotations;

namespace Blogger.Web.Models.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
        public required string Password { get; set; }
    }
}
