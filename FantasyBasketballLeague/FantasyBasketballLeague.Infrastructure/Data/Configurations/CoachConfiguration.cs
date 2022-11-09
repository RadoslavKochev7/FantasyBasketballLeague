using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasData(
            new Coach()
            {
                Id = 12,
                FirstName = "Mike",
                LastName = "Krzyzewski",
                TeamId = 1,
                ImageUrl = "https://cdn.britannica.com/72/212572-050-12A50228/Duke-University-mens-basketball-Mike-Krzyzewski.jpg"
            },
            new Coach()
            {
                Id = 13,
                FirstName = "Phil",
                LastName = "Jackson",
                TeamId = 2,
                ImageUrl = "https://cdn.britannica.com/38/219738-050-6AC916F7/American-basketball-player-and-coach-Phil-Jackson-2010.jpg"
            }
            );
        }
    }
}
