using Blogger.Web.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var identityUser = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Email
            };
            var identityResult = await userManager.CreateAsync(identityUser, register.Password);

            if (identityResult.Succeeded)
            {
                // assign this user the "User" role
                var identityRoleResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (identityRoleResult.Succeeded)
                {
                    // Show success notification
                    return RedirectToAction("Login");
                }
            }

            // Show error notification
            return View();
        }
    }
}
