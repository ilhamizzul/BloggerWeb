using Blogger.Web.Models.ViewModels.Blog;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blog;
        private readonly IBlogPostLikeRepository blogPostLike;

        public BlogsController(IBlogPostsRepository blog, IBlogPostLikeRepository blogPostLike)
        {
            this.blog = blog;
            this.blogPostLike = blogPostLike;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogData = await blog.GetByUrlHandleAsync(urlHandle);

            var blogDetailsViewModel = new BlogDetailsViewModel
            {
                Id = blogData.Id,
                Heading = blogData.Heading,
                PageTitle = blogData.PageTitle,
                Content = blogData.Content,
                ShortDescription = blogData.ShortDescription,
                FeaturedImageUrl = blogData.FeaturedImageUrl,
                UrlHandle = blogData.UrlHandle,
                PublishedDate = blogData.PublishedDate,
                Author = blogData.Author,
                Visible = blogData.Visible,
                Tags = blogData.Tags,
                TotalLikes = await blogPostLike.GetTotalLikes(blogData.Id)
            };

            return View(blogDetailsViewModel);
        }
    }
}
