namespace Blogger.Web.Models.ViewModels.Auth
{
    public class LoginViewModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
