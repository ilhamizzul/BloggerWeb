using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await _bloggerDbContext.Tags.AddAsync(tag);
            await _bloggerDbContext.SaveChangesAsync();
            
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _bloggerDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            // 1st Method
            //var tag = _bloggerDbContext.Tags.Where(data => data.Id == Id).FirstOrDefault();

            // 2nd Method
            var tag = await _bloggerDbContext.Tags.FindAsync(Id);

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
        public async Task<IActionResult> Edit(EditTagRequests request)
        {
            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name.Replace(" ", "-"),
                DisplayName = request.DisplayName
            };

            var existingTag = await _bloggerDbContext.Tags.FindAsync(request.Id);
            if (existingTag == null)
            {
                
                return RedirectToAction("Edit", new { Id = request.Id });
            }

            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;
            await _bloggerDbContext.SaveChangesAsync();    
            
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequests request)
        {
            var tag = await _bloggerDbContext.Tags.FindAsync(request.Id);
            if (tag == null)
            {
                return RedirectToAction("Edit", new { Id = request.Id });
            }
            _bloggerDbContext.Tags.Remove(tag);
            await _bloggerDbContext.SaveChangesAsync();
            
            return RedirectToAction("List");
        }
    }
}
