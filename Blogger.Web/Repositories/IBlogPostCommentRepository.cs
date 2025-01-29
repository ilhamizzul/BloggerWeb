using Blogger.Web.Models.Domain;

namespace Blogger.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> CommentsByBlogIdAsync(Guid blogPostId);
    }
}
