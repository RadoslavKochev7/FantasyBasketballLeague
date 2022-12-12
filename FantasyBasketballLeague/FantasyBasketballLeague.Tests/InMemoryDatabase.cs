using FantasyBasketballLeague.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Tests
{
    public static class InMemoryDatabase
    {
        public static FantasyLeagueDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<FantasyLeagueDbContext>()
                .UseInMemoryDatabase(databaseName: "FantasyLeagueInMemoryDb")
                .Options;

                return new FantasyLeagueDbContext(options);
            }
        }
    }
}
