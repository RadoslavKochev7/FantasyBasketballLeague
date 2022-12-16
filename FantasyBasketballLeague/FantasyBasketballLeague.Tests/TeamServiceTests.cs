using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using System.Security.Claims;

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
            Assert.DoesNotThrowAsync(async () => await teamService.GetByIdAsync(teamId));
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
        public async Task Test_AddAsync_Positive()
        {
            var teamsCount = dbContext.Teams.Count();
            var model = new TeamAddModel() { Id = 11, Name = "test", LogoUrl = ""};
            await teamService.AddAsync(model);

            var teams = await teamService.GetAllTeamsAsync();

            Assert.That(teamsCount, Is.LessThan(teams.Count()));
            Assert.That(teams.Any(t => t.Id == 11));
        }

        [Test]
        public void Test_AddAsync_NegativeScenario()
        {
#nullable disable

            var model1 = new TeamAddModel() { Id = 11, Name = "", LogoUrl = "" };
            var model2 = new TeamAddModel() { Id = 11, Name = null, LogoUrl = "" };

            Assert.ThrowsAsync<ArgumentNullException>
                (async () => await teamService.AddAsync(model1));

            Assert.ThrowsAsync<ArgumentNullException>
               (async () => await teamService.AddAsync(model2));

            Assert.ThrowsAsync<ArgumentNullException>
               (async () => await teamService.AddAsync(null));
        }

        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            var model = new Team() { Id = 15, Name = "team", LogoUrl = "" };
            await repo.AddAsync(model);
            await repo.SaveChangesAsync();

            var teamsInDb = dbContext.Teams.Count();
            await teamService.DeleteAsync(model.Id);

            var teams = await teamService.GetAllTeamsAsync();

            Assert.That(teams.Count(), Is.LessThan(teamsInDb));
            Assert.That(!teams.Any(p => p.Id == model.Id));
            Assert.IsFalse(model.IsActive);
        }

        [Test]
        public async Task Test_DeleteAsync_NotSuccessfullWithInvalidId()
        {
            var model = new TeamAddModel() { Id = 15, Name = "team", LogoUrl = "" };

            var teamsInDb = dbContext.Teams.Count();
            var teams = await teamService.GetAllTeamsAsync();

            await teamService.DeleteAsync(model.Id);

            Assert.That(teams.Count(), Is.EqualTo(teamsInDb));
        }

        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            var id = 10;
            var model = new TeamAddModel
            {
                Id = 30,
                Name = "team",
                LogoUrl = "url",
                CoachId = 2,
                LeagueId = 3
            };

            var teamId = await teamService.Edit(id, model);
            var editedTeam = await teamService.GetByIdAsync(teamId);

            Assert.Multiple(() =>
            {
                Assert.That(editedTeam.Name, Is.EqualTo(model.Name));
                Assert.That(editedTeam.LogoUrl, Is.EqualTo(model.LogoUrl));
                Assert.That(editedTeam.CoachId, Is.EqualTo(model.CoachId));
                Assert.That(editedTeam.LeagueId, Is.EqualTo(model.LeagueId));
                Assert.That(id, Is.EqualTo(teamId));
            });
        }

        [Test]
        public void Test_Edit_Negative_WithNotExistingObject()
        {
            var model = new TeamAddModel
            {
                Id = 30,
                Name = "team",
                LogoUrl = "url",
                CoachId = 2,
                LeagueId = 3
            };

            var id = 50;

            Assert.ThrowsAsync<NullReferenceException>(
                async () => await teamService.Edit(id, model));

            Assert.ThrowsAsync<NullReferenceException>(
                async () => await teamService.Edit(id, null));
        }


        [Test]
        public async Task Test_GetAllTeamsWithoutCoaches_Positive()
        {
            var model = new TeamAddModel
            {
                Id = 30,
                Name = "team",
                LogoUrl = "url",
                CoachId = null,
                LeagueId = 3
            };

            var userId = Guid.NewGuid().ToString();
            await teamService.AddAsync(model, userId);
            var teamsWithoutcoaches = await teamService.GetAllTeamsWithoutCoaches();

            Assert.That(teamsWithoutcoaches.Any(x => x.Id == model.Id));
        }

        [Test]
        public async Task Test_GetAllTeamsWithoutCoaches_Negative()
        {
            var model = new TeamAddModel
            {
                Id = 30,
                Name = "team",
                LogoUrl = "url",
                CoachId = 2,
                LeagueId = 3
            };

            await teamService.AddAsync(model);
            var teamsWithoutcoaches = await teamService.GetAllTeamsWithoutCoaches();

            Assert.That(teamsWithoutcoaches.Any(x => x.Id == model.Id), Is.False);
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
