using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blog;

        public BlogsController(IBlogPostsRepository blog)
        {
            this.blog = blog;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogData = await blog.GetByUrlHandleAsync(urlHandle);

            return View(blogData);
        }
    }
}
