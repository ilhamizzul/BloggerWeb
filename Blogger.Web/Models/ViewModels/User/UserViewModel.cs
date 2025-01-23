namespace Blogger.Web.Models.ViewModels.User
{
    public class UserViewModel
    {
        public List<User>? Users { get; set; }

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
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
