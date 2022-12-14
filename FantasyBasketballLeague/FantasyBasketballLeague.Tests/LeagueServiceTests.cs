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
        public async Task TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);
            leagueService = new LeagueService(repo);

            await SeedLeague(repo);
        }

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {
            int leagueId = 10;
            var league = await leagueService.GetByIdAsync(leagueId);

            Assert.NotNull(league);
            Assert.Multiple(() =>
            {
                Assert.That(league?.Name, Is.EqualTo("champions"));
                Assert.That(league?.Id, Is.EqualTo(leagueId));
            });
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            int leagueId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await leagueService.GetByIdAsync(leagueId));
        }

        [Test]
        public async Task Test_GetAllLeagues_GetsCorrectNumberOfLeagues_WithCorrectOrder()
        {
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
            var count = dbContext.Leagues.Count();
            var leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.EqualTo(count));

            var league = new League { Id = 14, Name = "Champions League" };
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
            var modelWithNullName = new LeagueViewModel { Name = null};

            Assert.ThrowsAsync<InvalidDataException>(
                async () => await leagueService.AddAsync(modelWithNullName));
            
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await leagueService.AddAsync(null));
        }


        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            var league = new LeagueViewModel { Id = 10, Name = "league"};
            await leagueService.AddAsync(league);
            var contexLeaguesCount = dbContext.Leagues.Count();
            var leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.EqualTo(contexLeaguesCount));

            await leagueService.DeleteAsync(league.Id);
            leagues = await leagueService.GetAllLeaguesAsync();

            Assert.That(leagues.Count(), Is.LessThan(contexLeaguesCount));
            Assert.That(!leagues.Any(p => p.Id == league.Id));
        }

        [Test]
        public void Test_DeleteAsync_NegativeScenario()
        {
            int id = 50;

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await leagueService.DeleteAsync(id));
        }


        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            var league = new LeagueViewModel { Id = 10, Name = "champions" };
            await leagueService.AddAsync(league);

            var model = new LeagueViewModel { Id = 10, Name = "Liga" };

            var leagueId = await leagueService.Edit(league.Id, model);
            var editedLeague = await leagueService.GetByIdAsync(leagueId);

            Assert.That(editedLeague.Name, Is.EqualTo(model.Name));
            Assert.That(league.Id, Is.EqualTo(leagueId));
        }

        [Test]
        public void Test_Edit_Negative_WithNotExistingObject()
        {
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

        private async Task SeedLeague(IRepository repo)
        {
            var league = new League { Id = 10, Name = "champions" };

            await repo.AddAsync(league);
            await repo.SaveChangesAsync();
        }
    }
}
