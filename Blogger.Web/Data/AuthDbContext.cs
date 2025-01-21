using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "5562b051-0ff9-4913-a3b8-761ae98a893d";
            var superAdminRoleId = "1b6e78b4-7ca4-4fee-8378-fb0ab2c984c4";
            var userRoleId = "7227d8fa-903f-49dc-9fbc-b9ea47ef76fa";

            var roles = new List<IdentityRole> {
                new IdentityRole {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin".ToUpper(),
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole {
                    Name="Admin",
                    NormalizedName="Admin".ToUpper(),
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole {
                    Name="User",
                    NormalizedName="User".ToUpper(),
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            var SuperAdminUserId = "20398aba-59e7-493e-9c92-7e1f51583630";
            // Seed SuperAdmin account
            var superAdminUser = new IdentityUser
            {
                Id = SuperAdminUserId,
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
                    RoleId = adminRoleId,
                    UserId = SuperAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = SuperAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = SuperAdminUserId
                },
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
        }
    }
}