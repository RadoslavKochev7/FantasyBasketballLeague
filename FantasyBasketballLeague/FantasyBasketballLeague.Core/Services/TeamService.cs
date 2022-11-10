using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
    [Authorize]
    public class TeamService : ITeamService
    {
        private readonly IRepository repo;

        public TeamService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync()
        {
            return await repo.AllReadonly<Team>()
                .OrderByDescending(t => t.Id)
                .Select(t => new TeamViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    OpenPositions = t.OpenPositions,
                    League = t.League.Name,
                    LeagueId = t.LeagueId,
                    LogoUrl = t.LogoUrl,
                })
                .ToListAsync();

        }
    }
}
