using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Position;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class PositionServiceTests
    {
        private IEnumerable<Position> Positions;
        private FantasyLeagueDbContext DbContext;
        private Mock<IRepository> MockedRepo;

        [SetUp]
        public void TestInitialize()
        {
            Positions = new List<Position>()
            {
                new Position(){ Id = 1, Name = "Center", Initials = "C" },
                new Position(){ Id = 2, Name = "Guard", Initials = "G" },
                new Position(){ Id = 3, Name = "Wing", Initials = "W" }
            };

            DbContext = InMemoryDatabase.Instance;
            DbContext.AddRange(Positions);
            DbContext.SaveChanges();


        }

        [Test]
        public async Task Test_GetAllPositions()
        {
            int positionId = 1;

            var dbPosition = this.Positions
                .First(p => p.Id == positionId);

            MockedRepo = new Mock<IRepository>();
            MockedRepo
                .Setup(x => x.GetByIdAsync<Position>(positionId))
                .ReturnsAsync(dbPosition);
            
            var mockedService = new Mock<IPositionService>();
            mockedService
                .Setup(s => s.GetAllPositionsAsync())
                .ReturnsAsync(this.Positions.Select(p => new PositionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Initials = p.Initials
                }));

            IPositionService service = new PositionService(MockedRepo.Object);

            var position = await service.GetByIdAsync(positionId);
            var positions = await service.GetAllPositionsAsync();

            Assert.True(position != null);
            Assert.Multiple(() =>
            {
                Assert.That(position?.Name == dbPosition.Name);
                Assert.That(position?.Initials == dbPosition.Initials);
                Assert.That(positions.Count(), Is.EqualTo(3));
            });
        }
    }
}