
using Blogger.Web.Data;
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
        public async Task<int> GetTotalLikes(Guid BlogId)
        {
            return await bloggerDb.BlogPostLike.CountAsync(x => x.BlogPostId == BlogId);
        }
    }
}
