using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<IdentityUser> CreateUsers()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "05a1e706-e884-447c-8152-6f67231e2e10",
                UserName = "player12",
                NormalizedUserName = "PLAYER12",
                Email = "player12@mail.com",
                NormalizedEmail = "PLAYER12.COM"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "player123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "7e170021-8670-45dc-8352-67d285dbd759",
                UserName = "guest123",
                NormalizedUserName = "GUEST123",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }
    }
}
