using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasData(
            new League()
            {
                Id = 1,
                Name = "Fantasy League",
            },
            new League()
            {
                Id = 2,
                Name = "Amateur League",
            });
        }
    }
}
