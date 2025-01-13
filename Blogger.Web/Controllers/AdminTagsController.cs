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
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            _bloggerDbContext.Tags.Add(tag);
            _bloggerDbContext.SaveChanges();
            
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = _bloggerDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            // 1st Method
            //var tag = _bloggerDbContext.Tags.Where(data => data.Id == Id).FirstOrDefault();

            // 2nd Method
            var tag = _bloggerDbContext.Tags.Find(Id);

            //3rd Method
            //var Tag = _bloggerDbContext.Tags.FirstOrDefault(data => data.Id == Id);

            if (tag == null)
            {
                return NotFound();
            }

            var editTagRequest = new EditTagRequests
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            //return View(tag);
            return View(editTagRequest);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequests request)
        {
            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name.Replace(" ", "-"),
                DisplayName = request.DisplayName
            };

            var existingTag = _bloggerDbContext.Tags.Find(request.Id);
            if (existingTag == null)
            {
                
                return RedirectToAction("Edit", new { Id = request.Id });
            }

            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;
            _bloggerDbContext.SaveChanges();
            
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequests request)
        {
            var tag = _bloggerDbContext.Tags.Find(request.Id);
            if (tag == null)
            {
                return RedirectToAction("Edit", new { Id = request.Id });
            }
            _bloggerDbContext.Tags.Remove(tag);
            _bloggerDbContext.SaveChanges();
            
            return RedirectToAction("List");
        }
    }
}
