using System.ComponentModel.DataAnnotations;

namespace Blogger.Web.Models.ViewModels.User
{
    public class UserViewModel
    {
        public List<User>? Users { get; set; }

        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public bool AdminRoleCheckbox{ get; set; }

    }

    public class User
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string EmailAddress { get; set; }
    }
}
