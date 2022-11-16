using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Models.League;
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

            var teamCoach = await repo.GetByIdAsync<Coach>(model.CoachId ?? 0);
            var teamLeague = await repo.GetByIdAsync<League>(model.LeagueId ?? 0);

            team.Coach = teamCoach;
            team.League = teamLeague;

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync()
        {
            return await repo.AllReadonly<Coach>()
                .Where(c => c.Team == null)
                .Select(c => new CoachViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync()
        {
            return await repo.AllReadonly<League>()
                .Select(l => new LeagueViewModel()
                {
                    Id = l.Id,
                    Name = l.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync()
        {
            var teams = await repo.All<Team>()
             .Include(t => t.Coach)
             .Include(t => t.League)
             .Select(t => new TeamViewModel()
             {
                 Id = t.Id,
                 Name = t.Name,
                 League = t.League.Name ?? "No league assigned",
                 LeagueId = t.LeagueId,
                 LogoUrl = t.LogoUrl,
                 CoachId = t.CoachId,
                 CoachName = $"{t.Coach.FirstName[0]}.{t.Coach.LastName}" ?? "No coach assigned",
                 OpenPositions = t.OpenPositions - t.Players.Count()
             })
             .ToListAsync();

            return teams;
        }

        public async Task<bool> TeamExists(int teamId)
        => await repo.AllReadonly<Team>().AnyAsync(t => t.Id == teamId);
    }
}
