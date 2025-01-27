using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels.Blog;

namespace Blogger.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid BlogId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike request);
    }
}
