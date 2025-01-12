using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
