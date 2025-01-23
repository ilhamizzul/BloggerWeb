using System.ComponentModel.DataAnnotations;

namespace Blogger.Web.Models.ViewModels.Tags
{
    public class AddTagRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string DisplayName { get; set; }
    }
}
