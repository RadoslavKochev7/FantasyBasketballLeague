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

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await playerService.GetByIdAsync(playerId));
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

            var model2 = new BasketballPlayerViewModel() { Id = 17, FirstName = "firstname", LastName = null };
            var model3 = new BasketballPlayerViewModel() { Id = 17, FirstName = "firstname", LastName = "last", JerseyNumber = null };
            var model4 = new BasketballPlayerViewModel() { Id = 17, FirstName = "firstname", LastName = "last"};

            Assert.ThrowsAsync<ArgumentException>
                (async () => await playerService.AddAsync(model1));

            Assert.ThrowsAsync<ArgumentException>
               (async () => await playerService.AddAsync(model2));

            Assert.ThrowsAsync<ArgumentException>
               (async () => await playerService.AddAsync(model3));

            Assert.ThrowsAsync<ArgumentException>
               (async () => await playerService.AddAsync(model4));

            Assert.ThrowsAsync<ArgumentNullException>
               (async () => await playerService.AddAsync(null));
        }

        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            var model = new BasketballPlayer()
            {
                Id = 17,
                FirstName = "test",
                LastName = "test",
                JerseyNumber = "6",
                IsStarter = true,
                IsTeamCaptain = false,
                SeasonsPlayed = 7,
                TeamId = 6,
                PositionId = 10,
            };
            await repo.AddAsync(model);
            await repo.SaveChangesAsync();

            var playersInDb = dbContext.BasketballPlayers.Count();
            await playerService.DeleteAsync(model.Id);

            var players = await playerService.GetAllPlayersAsync();

            Assert.That(players.Count(), Is.LessThan(playersInDb));
            Assert.That(!players.Any(p => p.Id == model.Id));
            Assert.IsFalse(model.IsActive);
        }

        [Test]
        public async Task Test_DeleteAsync_NotSuccessfullWithInvalidId()
        {
            var model = new TeamAddModel() { Id = 15, Name = "team", LogoUrl = "" };

            var playersInDb = dbContext.BasketballPlayers.Count();
            var players = await playerService.GetAllPlayersAsync();

            await playerService.DeleteAsync(model.Id);

            Assert.That(players.Count(), Is.EqualTo(playersInDb));
        }

        [Test]
        public async Task Test_PlayerNameExistsMethod()
        {
            var player = new BasketballPlayerViewModel()
            {
                Id = 17,
                FirstName = "firstName",
                LastName = "saas",
                JerseyNumber = "8",
                IsStarter = "true",
                IsTeamCaptain = "false"
            };

            await playerService.AddAsync(player);

            Assert.IsTrue(await playerService.PlayerNameExists(player.FirstName, player.LastName));
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
