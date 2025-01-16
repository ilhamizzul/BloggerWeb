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

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            var existingBlog = await bloggerDbContext.BlogPosts.FindAsync(Id);

            if (existingBlog != null)
            {
                bloggerDbContext.BlogPosts.Remove(existingBlog);
                await bloggerDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggerDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid Id)
        {
            var post =  await bloggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == Id);
            
            if (post == null)
            {
                return null;
            }

            return post;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingPost = await bloggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingPost != null)
            {
                existingPost.Id = blogPost.Id;
                existingPost.Heading = blogPost.Heading;
                existingPost.PageTitle = blogPost.PageTitle;
                existingPost.Content = blogPost.Content;
                existingPost.ShortDescription = blogPost.ShortDescription;
                existingPost.Author = blogPost.Author;
                existingPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingPost.UrlHandle = blogPost.UrlHandle;
                existingPost.PublishedDate = blogPost.PublishedDate;
                existingPost.Visible = blogPost.Visible;
                existingPost.Tags = blogPost.Tags;

                await bloggerDbContext.SaveChangesAsync();
                return existingPost;
            }

            return null;
        }
    }
}
