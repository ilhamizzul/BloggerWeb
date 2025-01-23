using Microsoft.AspNetCore.Identity;

namespace Blogger.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();
    }
}
