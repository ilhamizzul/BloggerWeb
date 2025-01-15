using Blogger.Web.Models.Domain;

namespace Blogger.Web.Repositories
{
    public interface IBlogPostsRepository
    {
        public Task<IEnumerable<BlogPost>> GetAllAsync();
        public Task<BlogPost> GetAsync(Guid Id);
        public Task<BlogPost> AddAsync(BlogPost blogPost);
        public Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        public Task<BlogPost?> DeleteAsync(Guid Id);
    }
}
