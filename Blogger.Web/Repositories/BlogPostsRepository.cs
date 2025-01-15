using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private readonly BloggerDbContext bloggerDbContext;

        public BlogPostsRepository(BloggerDbContext bloggerDbContext)
        {
            this.bloggerDbContext = bloggerDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggerDbContext.AddAsync(blogPost);
            await bloggerDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost> AddBlogAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggerDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public Task<BlogPost> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
