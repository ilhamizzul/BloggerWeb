﻿using Blogger.Web.Models.Domain;

namespace Blogger.Web.Repositories
{
    public interface ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllTagsAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null);
        public Task<Tag?> GetTagAsync(Guid Id);
        public Task<Tag> AddTagAsync(Tag Tag);
        public Task<Tag?> UpdateTagAsync(Tag tag);
        public Task<Tag?> DeleteTagAsync(Guid Id);

    }
}
