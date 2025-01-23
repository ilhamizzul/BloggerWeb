using Blogger.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDb;

        public UserRepository(AuthDbContext AuthDb)
        {
            authDb = AuthDb;
        }
        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            var users =  await authDb.Users.ToListAsync();
            var superAdminUser = await authDb.Users.FirstOrDefaultAsync(u => u.Email == "superadmin@blogger.com");

            if (superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
