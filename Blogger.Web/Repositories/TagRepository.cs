using Azure;
using Azure.Core;
using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;

        public TagRepository(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }
        public async Task<Tag> AddTagAsync(Tag Tag)
        {
            await _bloggerDbContext.Tags.AddAsync(Tag);
            await _bloggerDbContext.SaveChangesAsync();
            return Tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid Id)
        {
            var tag = await _bloggerDbContext.Tags.FindAsync(Id);
            if (tag != null)
            {
                _bloggerDbContext.Tags.Remove(tag);
                await _bloggerDbContext.SaveChangesAsync();
                return tag;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync(string? searchQuery, string? sortBy, string? sortDirection)
        {
            var query = _bloggerDbContext.Tags.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(data => data.Name.Contains(searchQuery) || data.DisplayName.Contains(searchQuery));
            }

            // sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(data => data.Name) : query.OrderBy(data => data.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(data => data.DisplayName) : query.OrderBy(data => data.DisplayName);
                }

            }

            // pagination

            return await query.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(Guid Id)
        {
            // 1st Method
            //var tag = _bloggerDbContext.Tags.Where(data => data.Id == Id).FirstOrDefault();

            // 2nd Method
            var tag = await _bloggerDbContext.Tags.FindAsync(Id);

            //3rd Method
            //var Tag = _bloggerDbContext.Tags.FirstOrDefault(data => data.Id == Id);

            if (tag == null) { return null; }

            return tag;
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await _bloggerDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _bloggerDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;

         
        }
    }
}
