namespace Blogger.Web.Models.ViewModels
{
    public class EditTagRequests
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
    }
}
