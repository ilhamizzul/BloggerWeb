using System.Diagnostics;
using Blogger.Web.Models;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostsRepository blogPosts;

        public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogPosts)
        {
            _logger = logger;
            this.blogPosts = blogPosts;
        }

        public async Task<IActionResult> Index()
        {
            var dataBlog = await blogPosts.GetAllAsync();
            return View(dataBlog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
