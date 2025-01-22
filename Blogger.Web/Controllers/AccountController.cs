using Blogger.Web.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn)
        {
            this.userManager = userManager;
            this.signInManager = signIn;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Password = "",
                Username = ""
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var signInResult =  await signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(login.ReturnUrl))
                {
                    return Redirect(login.ReturnUrl);

                }
                // Login successful
                return RedirectToAction("Index", "Home");
            }
            // Login failed
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
