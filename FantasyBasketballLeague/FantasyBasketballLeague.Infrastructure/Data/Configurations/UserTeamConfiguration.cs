using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(k => new
            {
                k.UserId,
                k.TeamId
            });

            builder.HasData(new UserTeam()
            {
                TeamId = 1,
                UserId = "05a1e706-e884-447c-8152-6f67231e2e10"
            });
        }
    }
}
