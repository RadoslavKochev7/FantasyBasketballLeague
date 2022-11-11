using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Authorization;

namespace FantasyBasketballLeague.Core.Services
{
    [Authorize]
    public class LeagueService : ILeagueService
    {
        private readonly IRepository repo;

        public LeagueService(IRepository _repo)
        {
            repo = _repo;
        }
    }
}
