using Blogger.Web.Models.ViewModels.User;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers.Admin
{
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository user;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository user, UserManager<IdentityUser> userManager)
        {
            this.user = user;
            this.userManager = userManager;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userRequest)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = userRequest.Username,
                    Email = userRequest.Email
                };
                var result = await userManager.CreateAsync(identityUser, userRequest.Password);

                if (result is not null && result.Succeeded)
                {
                    var role = new List<string> { "User" };
                    if (userRequest.AdminRoleCheckbox)
                    {
                        role.Add("Admin");
                    }

                    var identityUserRoleResult = await userManager.AddToRolesAsync(identityUser, role);

                    if (identityUserRoleResult.Succeeded && identityUserRoleResult is not null)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
            return RedirectToAction("List", "AdminUsers");
        }
    }
}
