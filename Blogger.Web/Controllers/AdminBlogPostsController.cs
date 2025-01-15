using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
