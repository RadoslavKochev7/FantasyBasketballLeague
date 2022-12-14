using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;

namespace FantasyBasketballLeague.Tests
{
    public class PlayerServiceTests
    {
        private FantasyLeagueDbContext dbContext;
        private IRepository repo = null!;
        private IPlayerService playerService = null!;

        [SetUp]
        public async Task TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);
            playerService = new PlayerService(repo);

            await SeedPlayer(repo);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedPlayer(IRepository repo)
        {
            var player = new BasketballPlayer()
            {
                Id = 30,
                FirstName = "Pesho",
                LastName = "Peshev",
                IsStarter = true,
                IsTeamCaptain = false,
                JerseyNumber = "33",
                SeasonsPlayed = 7,
                TeamId = 5,
                PositionId = 3,
            };

            await repo.AddAsync(player);
            await repo.SaveChangesAsync();
        }
    }
}
