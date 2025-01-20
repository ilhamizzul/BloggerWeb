using System.Diagnostics;
using Blogger.Web.Models;
using Blogger.Web.Models.ViewModels;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostsRepository blogPosts;
        private readonly ITagRepository tag;

        public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogPosts, ITagRepository tag)
        {
            _logger = logger;
            this.blogPosts = blogPosts;
            this.tag = tag;
        }

        public async Task<IActionResult> Index()
        {
            // GET all blog posts
            var dataBlogs = await blogPosts.GetAllAsync();
            // GET all tags
            var dataTags = await tag.GetAllTagsAsync();
            // Create a new instance of HomeViewModel
            var homeViewModel = new HomeViewModel
            {
                BlogPosts = dataBlogs,
                Tags = dataTags
            };

            return View(homeViewModel);
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
