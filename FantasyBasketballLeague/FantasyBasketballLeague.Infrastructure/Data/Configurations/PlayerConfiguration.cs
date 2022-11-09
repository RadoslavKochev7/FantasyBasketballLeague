using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyBasketballLeague.Infrastructure.Data.Configurations
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<BasketballPlayer>
    {
        public void Configure(EntityTypeBuilder<BasketballPlayer> builder)
        {
            builder.HasData(InitialSeedWithPlayers());
        }

        private List<BasketballPlayer> InitialSeedWithPlayers()
        {
            var players = new List<BasketballPlayer>()
            {
                new BasketballPlayer()
                {
                    Id = 1,
                    FirstName = "Radoslav",
                    LastName = "Kochev",
                    PositionId = 2,
                    TeamId = 1,
                    IsStarter = true,
                    IsTeamCaptain = true,
                    JerseyNumber = "7",
                    SeasonsPlayed = 12
                },
                new BasketballPlayer()
                {
                    Id = 2,
                    FirstName = "LeBron",
                    LastName = "James",
                    PositionId = 3,
                    TeamId = 1,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "6",
                    SeasonsPlayed = 18
                },
                new BasketballPlayer()
                {
                    Id = 3,
                    FirstName = "Magic",
                    LastName = "Johnson",
                    PositionId = 1,
                    TeamId = 1,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "32",
                    SeasonsPlayed = 14
                },
                new BasketballPlayer()
                {
                    Id = 4,
                    FirstName = "Alexander",
                    LastName = "Vezenkov",
                    PositionId = 4,
                    TeamId = 1,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "14",
                    SeasonsPlayed = 9
                },
                new BasketballPlayer()
                {
                    Id = 5,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    PositionId = 5,
                    TeamId = 1,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "15",
                    SeasonsPlayed = 9
                },
                 new BasketballPlayer()
                {
                    Id = 6,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    PositionId = 2,
                    TeamId = 2,
                    IsStarter = true,
                    IsTeamCaptain = true,
                    JerseyNumber = "23",
                    SeasonsPlayed = 16
                },
                new BasketballPlayer()
                {
                    Id = 7,
                    FirstName = "Luca",
                    LastName = "Doncic",
                    PositionId = 1,
                    TeamId = 2,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "77",
                    SeasonsPlayed = 5
                },
                new BasketballPlayer()
                {
                    Id = 8,
                    FirstName = "Dirk",
                    LastName = "Nowitzki",
                    PositionId = 4,
                    TeamId = 2,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "41",
                    SeasonsPlayed = 20
                },
                new BasketballPlayer()
                {
                    Id = 9,
                    FirstName = "Todor",
                    LastName = "Stoykov",
                    PositionId = 3,
                    TeamId = 2,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "10",
                    SeasonsPlayed = 16
                },
                new BasketballPlayer()
                {
                    Id = 10,
                    FirstName = "Shaquille",
                    LastName = "O'Neal",
                    PositionId = 5,
                    TeamId = 2,
                    IsStarter = true,
                    IsTeamCaptain = false,
                    JerseyNumber = "34",
                    SeasonsPlayed = 13
                }
            };

            return players;
        }
    }
}
