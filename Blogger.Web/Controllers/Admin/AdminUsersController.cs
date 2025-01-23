using Blogger.Web.Models.ViewModels.User;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers.Admin
{
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository user;

        public AdminUsersController(IUserRepository user)
        {
            this.user = user;
        }
        public async Task<IActionResult> List()
        {
            var users = await user.GetAllAsync();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var dataUser in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(dataUser.Id),
                    Username = dataUser.UserName,
                    EmailAddress = dataUser.Email
                });
            }
            return View(usersViewModel);
        }
    }
}
