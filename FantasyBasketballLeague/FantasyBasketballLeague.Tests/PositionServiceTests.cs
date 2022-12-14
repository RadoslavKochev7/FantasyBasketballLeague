using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Position;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class PositionServiceTests
    {
        private FantasyLeagueDbContext dbContext;
        private IRepository repo = null!;
        private IPositionService positionService = null!;

        [SetUp]
        public async Task TestInitialize()
        {
            dbContext = InMemoryDatabase.Instance;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            await SeedPosition(repo);
        }

        [Test]
        public async Task Test_GetByIdAsync_Positive()
        {
            int positionId = 10;
            var position = await positionService.GetByIdAsync(positionId);

            Assert.NotNull(position);
            Assert.Multiple(() =>
            {
                Assert.That(position?.Name == "Guard");
                Assert.That(position?.Initials == "PG");
                Assert.That(position?.Id == positionId);
            });
        }

        [Test]
        public void Test_GetByIdAsync_NegativeCase()
        {
            int positionId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await positionService.GetByIdAsync(positionId));
        }

        [Test]
        public async Task Test_GetAllPositions_GetsCorrectNumberOfPositions_WithCorrectOrder()
        {
            int positionId = 10;
            int count = dbContext.Positions.Count();

            var position = await positionService.GetByIdAsync(positionId);
            var positions = await positionService.GetAllPositionsAsync();
            var searchedPositionByName = positions.First().Name;

            Assert.NotNull(position);
            Assert.That(searchedPositionByName, Is.EqualTo(position?.Name));
            Assert.That(positions.Count(), Is.EqualTo(count));
        }

        [Test]
        public async Task Test_AddAsync_SuccessfullyAddsAnObject()
        {
            var count = dbContext.Positions.Count();
            var positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Count(), Is.EqualTo(count));

            var position = new PositionViewModel { Id = 10, Name = "Guard", Initials = "PG" };
            await positionService.AddAsync(position);

            positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Any(x => x.Id == position.Id));
            Assert.That(positions.Count(), Is.Not.EqualTo(count));
        }

        [Test]
        public void Test_AddAsync_NegativeScenarios()
        {
#nullable disable

            var modelWithNullName = new PositionViewModel { Name = null, Initials = "i" };
            var nullableModelInitials = new PositionViewModel { Name = "position", Initials = string.Empty };

            Assert.ThrowsAsync<InvalidDataException>(
                async () => await positionService.AddAsync(modelWithNullName));
            Assert.ThrowsAsync<InvalidDataException>(
                async () => await positionService.AddAsync(nullableModelInitials));
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await positionService.AddAsync(null));
        }

        [Test]
        public async Task Test_ExistsByName()
        {
            var position = new PositionViewModel { Id = 10, Name = "Guard", Initials = "PG" };
            await positionService.AddAsync(position);

            Assert.True(await positionService.ExistsByName(position.Name));
            Assert.False(await positionService.ExistsByName(position.Initials));
        }


        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            var position = new PositionViewModel { Id = 10, Name = "Guard", Initials = "PG" };
            await positionService.AddAsync(position);

            var dbPositionsCount = dbContext.Positions.Count();
            var positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Count(), Is.EqualTo(dbPositionsCount));

            await positionService.DeleteAsync(position.Id);
            positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Count(), Is.LessThan(dbPositionsCount));
            Assert.That(!positions.Any(p => p.Id == position.Id));
        }

        [Test]
        public void Test_DeleteAsync_NegativeScenario()
        {
            int id = 50;

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await positionService.DeleteAsync(id));
        }


        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            var model = new PositionViewModel { Name = "Position", Initials = "Pos" };
            var id = 10;

            var positionId = await positionService.Edit(id, model);
            var editedPosition = await positionService.GetByIdAsync(positionId);

            Assert.That(editedPosition.Name, Is.EqualTo(model.Name));
            Assert.That(editedPosition.Initials, Is.EqualTo(model.Initials));
            Assert.That(id, Is.EqualTo(positionId));
        }

        [Test]
        public void Test_Edit_Negative_WithNotExistingObject()
        {
            var model = new PositionViewModel { Name = "Position", Initials = "Pos" };
            var id = 50;

            Assert.ThrowsAsync<NullReferenceException>(
                async () => await positionService.Edit(id, model));

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await positionService.Edit(id, null));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedPosition(IRepository repo)
        {
            var Position = new Position 
            { 
                Id = 10, 
                Name = "Guard", 
                Initials = "PG" 
            };

            await repo.AddAsync(Position);
            await repo.SaveChangesAsync();
        }
    }
}