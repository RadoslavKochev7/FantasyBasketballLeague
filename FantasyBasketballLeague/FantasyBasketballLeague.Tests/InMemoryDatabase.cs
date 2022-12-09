using FantasyBasketballLeague.Infrastructure.Data;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
