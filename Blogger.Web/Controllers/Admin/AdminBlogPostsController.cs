using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels.Blog;
using Blogger.Web.Reports;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuestPDF.Fluent;

namespace Blogger.Web.Controllers.Admin
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
                UrlHandle = request.UrlHandle.Replace(" ", "-"),
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
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostsRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var post = await blogPostsRepository.GetAsync(Id);
            var tagsDomain = await tagRepository.GetAllTagsAsync();

            if (post == null)
            {
                return RedirectToAction("List");
            }

            var model = new EditBlogPostsRequest
            {
                Id = post.Id,
                Heading = post.Heading,
                PageTitle = post.PageTitle,
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                FeaturedImageUrl = post.FeaturedImageUrl,
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate,
                Author = post.Author,
                Visible = post.Visible,
                Tags = tagsDomain.Select(x => new SelectListItem
                {
                    Text = x.DisplayName,
                    Value = x.Id.ToString()
                }),
                SelectedTags = post.Tags.Select(x => x.Id.ToString()).ToArray()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostsRequest request)
        {
            // mapping domain model
            var post = new BlogPost
            {
                Id = request.Id,
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle.Replace(" ", "-"),
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                Visible = request.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var tagId in request.SelectedTags)
            {
                if (Guid.TryParse(tagId, out var parsedTagId))
                {
                    var existingTag = await tagRepository.GetTagAsync(parsedTagId);
                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }
                }

            }
            post.Tags = selectedTags;

            // submit information to repository to update
            var updatedPost = await blogPostsRepository.UpdateAsync(post);


            // redirect to GET
            if (updatedPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { post.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var deletedBlog = await blogPostsRepository.DeleteAsync(Id);
            if (deletedBlog != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { Id });
        }

        public async Task<ActionResult> Download()
        {
            var data = await blogPostsRepository.GetAllAsync();
            var document = new ReportPDF(data, "2025-01-16");

            var pdfBytes = document.GeneratePdf();
            var date = DateTime.Now.ToString("ddMMyy");
            return File(pdfBytes, "application/pdf", "BlogPosts_" + date + ".pdf");
        }
    }
}
