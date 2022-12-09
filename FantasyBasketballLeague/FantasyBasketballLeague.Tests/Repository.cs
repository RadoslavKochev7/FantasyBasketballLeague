using FantasyBasketballLeague.Infrastructure.Data.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyBasketballLeague.Tests
{
    public class Repository
    {
        public static Repository Instance
        {
            get
            {
                var repo = new Mock<Repository>(InMemoryDatabase.Instance);
                return repo.Object;
            }
        }
    }
}
