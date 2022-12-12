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
            positionService = new PositionService(repo);

            await repo.AddAsync(new Position { Id = 10, Name = "Guard", Initials = "PG" });
            await repo.SaveChangesAsync();

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
        public async Task Test_GetByIdAsync_NegativeCase()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            await repo.AddAsync(new Position { Id = 10, Name = "Guard", Initials = "PG" });
            await repo.SaveChangesAsync();

            int positionId = 124;

            Assert.ThrowsAsync<InvalidOperationException>
                (async () => await positionService.GetByIdAsync(positionId));
        }

        [Test]
        public async Task Test_GetAllPositions_GetsCorrectNumberOfPositions_WithCorrectOrder()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            await repo.AddAsync(new Position { Id = 10, Name = "Guard", Initials = "PG" });
            await repo.SaveChangesAsync();

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
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            var count = 5;
            var positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Count(), Is.EqualTo(count));

            var position = new Position { Id = 10, Name = "Guard", Initials = "PG" };
            await repo.AddAsync(position);
            var result = await repo.SaveChangesAsync();
            positions = await positionService.GetAllPositionsAsync();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(positions.Count(), Is.Not.EqualTo(count));
        }

        [Test]
        public void Test_AddAsync_NegativeScenarios()
        {
#nullable disable

            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

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
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            var position = new Position { Id = 10, Name = "Guard", Initials = "PG" };
            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

            Assert.True(await positionService.ExistsByName(position.Name));
            Assert.False(await positionService.ExistsByName(position.Initials));
        }


        [Test]
        public async Task Test_DeleteAsync_Positive()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            var position = new Position { Id = 10, Name = "Guard", Initials = "PG" };
            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

            var positions = await positionService.GetAllPositionsAsync();
            
            Assert.That(positions.Count(), Is.EqualTo(6));

            await positionService.DeleteAsync(position.Id);
            positions = await positionService.GetAllPositionsAsync();

            Assert.That(positions.Count(), Is.LessThan(6));
            Assert.That(!positions.Any(p => p.Id == position.Id), Is.True); 
        }

        [Test]
        public void Test_DeleteAsync_NegativeScenario()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            int id = 50;

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await positionService.DeleteAsync(id));
        }


        [Test]
        public async Task Test_Edit_PositiveScenario()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            var position = new Position { Id = 10, Name = "Guard", Initials = "PG" };
            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

            var model = new PositionViewModel{ Name = "Position", Initials = "Pos" };
            var id = 10;

            var positionId = await positionService.Edit(id, model);
            var editedPosition = await positionService.GetByIdAsync(positionId);

            Assert.That(editedPosition.Name, Is.EqualTo(model.Name));
            Assert.That(editedPosition.Initials, Is.EqualTo(model.Initials));
            Assert.That(id, Is.EqualTo(positionId));
        }

        [Test]
        public async Task Test_Edit_Negative_WithNotExistingObject()
        {
            repo = new Repository(dbContext);
            positionService = new PositionService(repo);

            var position = new Position { Id = 10, Name = "Guard", Initials = "PG" };
            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

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
    }
}