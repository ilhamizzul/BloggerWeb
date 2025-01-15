using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogger.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminBlogPostsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get Tag from repository
            var tags = await tagRepository.GetAllTagsAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {
            var blog = new BlogPost
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                Visible = request.Visible,
                //Tags = request.SelectedTag
            };

            return RedirectToAction("List", "AdminTags");
        }
    }
}
