using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles (User, Admin, SuperAdmin)

            //var adminRoleId = "b10b9221-4a67-40c4-a2fe-7028a4e3457f";
            var adminRoleId = Guid.NewGuid().ToString();
            var superAdminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            var roles = new List<IdentityRole> {
                new IdentityRole {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole {
                    Name="User",
                    NormalizedName="User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
            // Seed SuperAdmin account
            var superAdminUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "superadmin@blogger.com",
                Email = "superadmin@blogger.com",
                NormalizedEmail = "superadmin@blogger.com".ToUpper(),
                NormalizedUserName = "superadmin@blogger.com".ToUpper(),
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");

            modelBuilder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add Roles to SuperAdmin
            var superAdminRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = superAdminUser.Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = superAdminUser.Id,
                    RoleId = roles[1].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = superAdminUser.Id,
                    RoleId = roles[2].Id
                },
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
        }
    }
}
