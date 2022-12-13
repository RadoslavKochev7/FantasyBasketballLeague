using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private FantasyLeagueDbContext dbContext;
        private IRepository repo = null!;
        private ITeamService teamService = null!;

        [SetUp]
        public async Task TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);
            teamService = new TeamService(repo);

            await SeedTeam(repo);
        }

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {
            int teamId = 10;
            var searchedteam = await teamService.GetByIdAsync(teamId);

            Assert.NotNull(searchedteam);
            Assert.DoesNotThrowAsync(() => teamService.GetByIdAsync(teamId));
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            int teamId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await teamService.GetByIdAsync(teamId));
        }

        [Test]
        public async Task Test_GetAllTeams_GetsCorrectNumberOfTeams_WithCorrectOrder()
        {
            var team = new Team() { Id = 20, Name = "team", LogoUrl = "" };

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();

            int count = dbContext.Teams.Count();
            var teams = await teamService.GetAllTeamsAsync();

            Assert.That(teams.Count(), Is.EqualTo(count));

            team.IsActive = false;
            await repo.SaveChangesAsync();

            teams = await teamService.GetAllTeamsAsync();

            Assert.That(teams.Count(), Is.Not.EqualTo(count));
        }


        [Test]
        public void Test_AddAsync_Positive()
        {
            
        }

        [Test]
        public void Test_AddAsync_NegativeScenario()
        {
            int teamId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await teamService.GetByIdAsync(teamId));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedTeam(IRepository repo)
        {
            var team = new Team
            {
                Id = 10,
                Name = "Test",
                LogoUrl = "logoUrl",
                CoachId = 12,
                LeagueId = 3,
                IsActive = true
            };

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();
        }
    }
}
