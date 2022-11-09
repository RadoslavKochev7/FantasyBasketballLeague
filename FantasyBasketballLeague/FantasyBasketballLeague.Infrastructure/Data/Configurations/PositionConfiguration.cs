using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(
            new Position()
            {
                Id = 1,
                Name = "Point Guard",
                Initials = "PG"
            },
            new Position()
            {
                Id = 2,
                Name = "Shooting Guard",
                Initials = "SG"
            },
            new Position()
            {
                Id = 3,
                Name = "Small Forward",
                Initials = "SF"
            },
            new Position()
            {
                Id = 4,
                Name = "Power Forward",
                Initials = "PF"
            },
            new Position()
            {
                Id = 5,
                Name = "Center",
                Initials = "C"
            });

        }
    }
}
