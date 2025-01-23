namespace Blogger.Web.Models.ViewModels.User
{
    public class UserViewModel
    {
        public List<User>? Users { get; set; }
    }

    public class User
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string EmailAddress { get; set; }
    }
}
