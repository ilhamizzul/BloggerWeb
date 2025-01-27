﻿using Blogger.Web.Models.ViewModels.Blog;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blog;
        private readonly IBlogPostLikeRepository blogPostLike;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(
            IBlogPostsRepository blog, IBlogPostLikeRepository blogPostLike, 
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.blog = blog;
            this.blogPostLike = blogPostLike;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogData = await blog.GetByUrlHandleAsync(urlHandle);
            var liked = false;

            if (signInManager.IsSignedIn(User))
            {
                // Get like for this blog from this logged in user
                var allLikesforBlog = await blogPostLike.GetLikesFromUser(blogData.Id);
                var userId = userManager.GetUserId(User);
                if (userId != null)
                {
                    var userLike = allLikesforBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                    liked = userLike != null;
                }
            }


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
                TotalLikes = await blogPostLike.GetTotalLikes(blogData.Id),
                Liked = liked
            };

            return View(blogDetailsViewModel);
        }
    }
}
