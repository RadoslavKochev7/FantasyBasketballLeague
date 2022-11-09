using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasData(
            new Team()
            {
                Id = 1,
                Name = "Dream Team",
                CoachId = 2,
                LeagueId = 1,
                LogoUrl = "https://basketball.bg/pictures/pic_big/teams/20210929013457.png"
            },
            new Team()
            {
                Id = 2,
                Name = "The Lions",
                CoachId = 1,
                LeagueId = 1,
                LogoUrl = "https://pbs.twimg.com/profile_images/1214219629398286337/sgx5t_Qj_400x400.jpg"
            });
        }
    }
}
