using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace FantasyBasketballLeague.Tests
{
    [TestFixture]
    public class PositionTests
    {
        private IEnumerable<Position> positions;
        private FantasyLeagueDbContext dbContext;
        private Mock<IRepository> mockedRepo;

        [SetUp]
        public void TestInitialize()
        {
            mockedRepo = new Mock<IRepository>();
            positions = new List<Position>()
            {
                new Position(){Id = 1, Name = "Center", Initials = "C"},
                new Position(){Id = 2, Name = "Guard", Initials = "G" },
                new Position(){ Id = 3, Name = "Wing", Initials = "W" }
            };

            var options = new DbContextOptionsBuilder<FantasyLeagueDbContext>()
                    .UseInMemoryDatabase("FantasyLeagueInMemoryDb")
                    .Options;
            dbContext = new FantasyLeagueDbContext(options);
            dbContext.AddRange(positions);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task Test_GetAllPositions()
        {
            var positionId = 1;

            IPositionService service =
                new PositionService(mockedRepo.Object); // Pass it to Service as dependency
            var position = await service.GetByIdAsync(positionId);

            var dbDecision = positions
                .Select(p => p.Id)
                .ToList();

            Assert.True(position != null);
            Assert.That(position.Name == "Center");
            Assert.That(positions.Count(), Is.EqualTo(3));
        }
    }
}