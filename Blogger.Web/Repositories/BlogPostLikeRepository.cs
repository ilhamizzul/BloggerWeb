
using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels.Blog;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggerDbContext bloggerDb;

        public BlogPostLikeRepository(BloggerDbContext bloggerDb)
        {
            this.bloggerDb = bloggerDb;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike request)
        {
            await bloggerDb.BlogPostLike.AddAsync(request);
            await bloggerDb.SaveChangesAsync();
            return request;
        }

        public async Task<int> GetTotalLikes(Guid BlogId)
        {
            return await bloggerDb.BlogPostLike.CountAsync(x => x.BlogPostId == BlogId);
        }
    }
}
