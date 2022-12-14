using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Core.Models.Teams;
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

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {
            int playerId = 15;

            var searchedPlayer = await playerService.GetByIdAsync(playerId);

            Assert.NotNull(searchedPlayer);
            Assert.DoesNotThrowAsync(async () => await playerService.GetByIdAsync(playerId));
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            int playerId = 124;

            Assert.ThrowsAsync<NullReferenceException>
                (async () => await playerService.GetByIdAsync(playerId), message: "No such player.");
        }

        [Test]
        public async Task Test_GetAllPlayers_GetsCorrectNumberOfPlayers_WithCorrectOrder()
        {
            var player = dbContext.BasketballPlayers.First();
            player.Id = 17;

            await repo.AddAsync(player);
            await repo.SaveChangesAsync();

            int count = dbContext.BasketballPlayers.Count();
            var players = await playerService.GetAllPlayersAsync();

            Assert.That(players.Count(), Is.EqualTo(count));

            player.IsActive = false;
            await repo.SaveChangesAsync();

            players = await playerService.GetAllPlayersAsync();

            Assert.That(players.Count(), Is.LessThan(count));
        }

        [Test]
        public async Task Test_AddAsync_Positive()
        {
            var playersCount = dbContext.Teams.Count();
            var model = new BasketballPlayerViewModel() 
            {
                Id = 17, 
                FirstName = "test", 
                LastName = "test", 
                JerseyNumber = "6",
                IsStarter = "yes",
                IsTeamCaptain = "no",
                SeasonsPlayed = 7,
                TeamId = 6,
                PositionId = 10,
            };

            await playerService.AddAsync(model);

            var players = await playerService.GetAllPlayersAsync();

            Assert.That(playersCount, Is.LessThan(players.Count()));
            Assert.That(players.Any(t => t.Id == model.Id));
        }

        [Test]
        public void Test_AddAsync_NegativeScenario()
        {
#nullable disable

            var model1 = new BasketballPlayerViewModel() 
            { 
                Id = 17, 
                FirstName = "", 
                LastName = "saas", 
                JerseyNumber = "8", 
                IsStarter = "true", 
                IsTeamCaptain = "false" 
            };

            var model2 = new BasketballPlayerViewModel() { Id = 17, FirstName = null, LastName = "saas" };

            Assert.ThrowsAsync<ArgumentNullException>
                (async () => await playerService.AddAsync(model1));

            Assert.ThrowsAsync<ArgumentNullException>
               (async () => await playerService.AddAsync(model2));

            Assert.ThrowsAsync<ArgumentNullException>
               (async () => await playerService.AddAsync(null));
        }



        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedPlayer(IRepository repo)
        {
            var position = new Position()
            {
                Id = 10,
                Name = "dsadsad",
                Initials = "init"
            };

            var team = new Team()
            {
                Id = 6,
                Name = "nejm",
                LogoUrl = "",
            };

            var player = new BasketballPlayer()
            {
                Id = 15,
                FirstName = "Pesho",
                LastName = "Peshev",
                IsStarter = true,
                IsTeamCaptain = false,
                JerseyNumber = "33",
                SeasonsPlayed = 7,
                TeamId = 6,
                Team = team,
                Position = position,
                PositionId = 10,
            };

            await repo.AddAsync(player);
            await repo.SaveChangesAsync();
        }
    }
}
