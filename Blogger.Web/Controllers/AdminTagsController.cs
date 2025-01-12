using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggerDbContext _bloggerDbContext;
        public AdminTagsController(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest request)
        {
            var tag = new Tag { 
                Name = request.Name,
                DisplayName = request.DisplayName 
            };

            _bloggerDbContext.Tags.Add(tag);
            _bloggerDbContext.SaveChanges();
            _bloggerDbContext.Dispose();
            return View("Add");
        }
    }
}
