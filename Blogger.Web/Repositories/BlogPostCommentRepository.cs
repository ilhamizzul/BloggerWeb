using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggerDbContext bloggerDb;

        public BlogPostCommentRepository(BloggerDbContext bloggerDb)
        {
            this.bloggerDb = bloggerDb;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await bloggerDb.BlogPostComment.AddAsync(blogPostComment);
            await bloggerDb.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> CommentsByBlogIdAsync(Guid blogPostId)
        {
            return await bloggerDb.BlogPostComment.Where(c => c.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
