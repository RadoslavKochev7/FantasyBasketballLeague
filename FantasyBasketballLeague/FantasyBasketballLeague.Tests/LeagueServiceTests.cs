using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class LeagueServiceTests
    {
        private FantasyLeagueDbContext dbContext;
        private IRepository repo = null!;
        private ILeagueService leagueService = null!;

        [SetUp]
        public void TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            await repo.AddAsync(new League { Id = 10, Name = "Champs" });
            await repo.SaveChangesAsync();

            int leagueId = 10;
            var league = await leagueService.GetByIdAsync(leagueId);

            Assert.NotNull(league);
            Assert.Multiple(() =>
            {
                Assert.That(league?.Name == "Champs");
                Assert.That(league?.Id == leagueId);
            });
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            int leagueId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await leagueService.GetByIdAsync(leagueId));
        }

        [Test]
        public async Task Test_GetAllLeagues_GetsCorrectNumberOfLeagues_WithCorrectOrder()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            await repo.AddAsync(new League { Id = 10, Name = "league" });
            await repo.SaveChangesAsync();

            int id = 10;
            int count = dbContext.Leagues.Count();

            var league = await leagueService.GetByIdAsync(id);
            var leagues = await leagueService.GetAllLeaguesAsync();
            var searchedLeagueByName = leagues.First().Name;

            Assert.NotNull(league);
            Assert.That(searchedLeagueByName, Is.EqualTo(league?.Name));
            Assert.That(leagues.Count(), Is.EqualTo(count));
        }

        [Test]
        public async Task Test_AddAsync_SuccessfullyAddsAnObject()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            var count = 2;
            var leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.EqualTo(count));

            var league = new League { Id = 10, Name = "Champions League" };
            await repo.AddAsync(league);
            var result = await repo.SaveChangesAsync();
            leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(leagues.Count(), Is.Not.EqualTo(count));
        }

        [Test]
        public void Test_AddAsync_NegativeScenarios()
        {
#nullable disable

            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            var modelWithNullName = new LeagueViewModel { Name = null};

            Assert.ThrowsAsync<InvalidDataException>(
                async () => await leagueService.AddAsync(modelWithNullName));
            
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await leagueService.AddAsync(null));
        }


        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            var league = new League { Id = 10, Name = "league"};
            await repo.AddAsync(league);
            await repo.SaveChangesAsync();

            var leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.EqualTo(3));

            await leagueService.DeleteAsync(league.Id);
            leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.LessThan(3));
            Assert.That(!leagues.Any(p => p.Id == league.Id), Is.True);
        }

        [Test]
        public void Test_DeleteAsync_NegativeScenario()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            int id = 50;

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await leagueService.DeleteAsync(id));
        }


        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            var league = new League { Id = 10, Name = "champions" };
            await repo.AddAsync(league);
            await repo.SaveChangesAsync();

            var model = new LeagueViewModel { Id = 10, Name = "Liga" };

            var leagueId = await leagueService.Edit(league.Id, model);
            var editedLeague = await leagueService.GetByIdAsync(leagueId);

            Assert.That(editedLeague.Name, Is.EqualTo(model.Name));
            Assert.That(league.Id, Is.EqualTo(leagueId));
        }

        [Test]
        public async Task Test_Edit_Negative_WithNotExistingObject()
        {
            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            var league = new League { Id = 10, Name = "Liga"};
            await repo.AddAsync(league);
            await repo.SaveChangesAsync();

            var model = new LeagueViewModel { Id = 500, Name = "Seria A" };
            int id = 0;

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await leagueService.Edit(id, model));

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await leagueService.Edit(id, null));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
