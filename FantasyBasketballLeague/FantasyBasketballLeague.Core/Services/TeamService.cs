using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Teams;
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

        public async Task AddAsync(TeamAddModel model)
        {
            var team = new Team()
            {
                Id = model.Id,
                Name = model.Name,
                CoachId = model.CoachId.HasValue ? model.CoachId : null,
                LeagueId = model.LeagueId.HasValue ? model.LeagueId : null,
                LogoUrl = model.LogoUrl,
            };

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync()
        {
            var result = await repo.All<Team>()
                .Include(t => t.League)
                .Include(t => t.Coach)
                .OrderByDescending(t => t.Id)
                .Select(t => new TeamViewModel()
                {
                    Name = t.Name,
                    League = t.League.Name,
                    LogoUrl = t.LogoUrl,
                    CoachName = $"{t.Coach.FirstName[0]}.{t.Coach.LastName}" ?? "No coach assigned",
                    OpenPositions = t.OpenPositions - t.Players.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<bool> TeamExists(int teamId)
        => await repo.AllReadonly<Team>().AnyAsync(t => t.Id == teamId);
    }
}
