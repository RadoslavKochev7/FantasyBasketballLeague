using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class CoachServiceTests
    {
        private FantasyLeagueDbContext dbContext;
        private IRepository repo = null!;
        private ICoachService coachService = null!;

        [SetUp]
        public async Task TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);
            coachService = new CoachService(repo);
            await SeedCoach(repo);
        }

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {

            int CoachId = 30;
            var searchedCoach = await coachService.GetByIdAsync(CoachId);

            Assert.NotNull(searchedCoach);
            Assert.DoesNotThrowAsync(() => coachService.GetByIdAsync(CoachId));
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            int CoachId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await coachService.GetByIdAsync(CoachId));
        }

        [Test]
        public async Task Test_GetAllCoaches_GetsCorrectNumberOfCoaches_WithCorrectOrder()
        {
            var coach = new Coach() { Id = 16, FirstName = "Ivan", LastName = "Dimitrov", ImageUrl = "" };
            await repo.AddAsync(coach);
            await repo.SaveChangesAsync();

            int count = dbContext.Coaches.Count();
            var coaches = await coachService.GetAllCoachesAsync();

            Assert.That(coaches.Count(), Is.EqualTo(count));

            coach.IsActive = false;
            await repo.SaveChangesAsync();
            coaches = await coachService.GetAllCoachesAsync();

            Assert.That(coaches.Count(), Is.Not.EqualTo(count));
        }

        [Test]
        public async Task Test_AddAsync_SuccessfullyAddsAnObject()
        {
            var coachesCount = dbContext.Coaches.Count();
            var coaches = await coachService.GetAllCoachesAsync();

            Assert.That(coaches.Count(), Is.EqualTo(coachesCount));

            var model = new CoachViewModel() { Id = 16, FirstName = "Ivan", LastName = "Georgi" };
            await coachService.AddAsync(model);
            coaches = await coachService.GetAllCoachesAsync();

            Assert.That(coachesCount, Is.LessThan(coaches.Count()));
            Assert.That(coaches.Any(c => c.Id == model.Id));
        }

        [Test]
        public void Test_AddAsync_NegativeScenarios()
        {
#nullable disable

            var modelWithNullName = new CoachViewModel { FirstName = null, LastName = "idsadsa", ImageUrl = "dsadas" };
            var coachWithEmptyLastName = new CoachViewModel { FirstName = "Coach", LastName = "", ImageUrl = "dasdas" };

            Assert.ThrowsAsync<ApplicationException>(
                async () => await coachService.AddAsync(modelWithNullName));
            Assert.ThrowsAsync<ApplicationException>(
                async () => await coachService.AddAsync(coachWithEmptyLastName));
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await coachService.AddAsync(null));
        }

        [Test]
        public async Task Test_AvailableCoaches_Returns_AllCoachesWithoutTeamId()
        {
            var coach = new Coach()
            {
                Id = 23,
                FirstName = "Pesho",
                LastName = "Peshev",
                ImageUrl = "",
                TeamId = null
            };

            await repo.AddAsync(coach);
            await repo.SaveChangesAsync();
            var unassignedCoaches = await coachService.AvailableCoaches();

            Assert.True(unassignedCoaches.Any(c => c.Id == 23));

            coach.TeamId = 1;
            await repo.SaveChangesAsync();
            unassignedCoaches = await coachService.AvailableCoaches();

            Assert.False(unassignedCoaches.Any(c => c.Id == 23));
        }


        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            var coaches = await coachService.GetAllCoachesAsync();
            var coachesInDb = dbContext.Coaches.Count();

            Assert.That(coaches.Count(), Is.EqualTo(coachesInDb));

            var coachId = 30;
            await coachService.DeleteAsync(coachId);
            coaches = await coachService.GetAllCoachesAsync();

            Assert.That(coaches.Count(), Is.LessThan(coachesInDb));
            Assert.That(!coaches.Any(p => p.Id == coachId));
        }

        [Test]
        public void Test_DeleteAsync_NegativeScenario()
        {
            repo = new Repository(dbContext);
            coachService = new CoachService(repo);

            int id = 50;

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await coachService.DeleteAsync(id));
        }


        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            var model = new CoachDetailsModel
            { 
                Id = 30,
                FirstName = "coach",
                LastName = "Coachev",
                ImageUrl = "imageUrl",
                TeamId = 20
            };

            var id = model.Id;
            
            var coachId = await coachService.Edit(id, model);
            var editedCoach = await coachService.GetByIdAsync(coachId);

            Assert.Multiple(() =>
            {
                Assert.That(editedCoach.FirstName, Is.EqualTo(model.FirstName));
                Assert.That(editedCoach.LastName, Is.EqualTo(model.LastName));
                Assert.That(editedCoach.ImageUrl, Is.EqualTo(model.ImageUrl));
                Assert.That(editedCoach.TeamId, Is.EqualTo(model.TeamId));
                Assert.That(id, Is.EqualTo(coachId));
            });
        }

        [Test]
        public void Test_Edit_Negative_WithNotExistingObject()
        {
            var model = new CoachDetailsModel
            {
                Id = 30,
                FirstName = "coach",
                LastName = "Coachev",
                ImageUrl = "imageUrl",
                TeamId = 20
            };

            var id = 50;

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await coachService.Edit(id, model));

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await coachService.Edit(id, null));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedCoach(IRepository repo)
        {
            var coach = new Coach
            {
                Id = 30,
                FirstName = "Ivan",
                LastName = "Ivanov",
                ImageUrl = "",
                IsActive = true,
            };

            await repo.AddAsync(coach);
            await repo.SaveChangesAsync();
        }
    }
}
