using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels.Blog;
using Blogger.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLike;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLike)
        {
            this.blogPostLike = blogPostLike;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            await blogPostLike.AddLikeForBlog(new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            });

            return Ok();
        }
    }
}
