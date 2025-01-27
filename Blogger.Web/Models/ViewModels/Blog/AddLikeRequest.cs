namespace Blogger.Web.Models.ViewModels.Blog
{
    public class AddLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
