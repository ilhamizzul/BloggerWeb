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
        private readonly IBlogPostsRepository blogPostsRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostsRepository blogPostsRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostsRepository = blogPostsRepository;
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
            var blogPost = new BlogPost
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
            };
            // Map Tags from selected Tags
            var selectedTags = new List<Tag>();
            foreach (var tagId in request.SelectedTags)
            {
                var tagIdAsGuid = Guid.Parse(tagId);
                var existingTag = await tagRepository.GetTagAsync(tagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogPostsRepository.AddAsync(blogPost); 
            return RedirectToAction("Add");
        }
    }
}
