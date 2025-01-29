using Blogger.Web.Models.Domain;
using Microsoft.AspNetCore.Authentication;

namespace Blogger.Web.Models.ViewModels.Blog
{
    public class BlogDetailsViewModel
    {
        public Guid Id { get; set; }
        public required string Heading { get; set; }
        public required string PageTitle { get; set; }
        public required string Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public required string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public string CommentDescription { get; set; }
        public IEnumerable<BlogComment> Comments { get; set; }
    }
}
