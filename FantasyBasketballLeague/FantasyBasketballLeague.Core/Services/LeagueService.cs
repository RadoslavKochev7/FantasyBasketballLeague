using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IRepository repo;

        public LeagueService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(LeagueViewModel model)
        {
            var league = new League()
            {
                Id = model.Id,
                Name = model.Name
            };

            await repo.AddAsync(league);
            await repo.SaveChangesAsync();

            return league.Id;
        }

        public async Task AddTeam(int teamId, int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (team != null && league != null) 
            league.Teams.Add(team);

            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            if (league != null)
            {
                await repo.DeleteAsync<League>(leagueId);
                await repo.SaveChangesAsync();
            }
        }

        public async Task<int> Edit(int leagueId, LeagueViewModel model)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            if (leagueId == model.Id)
                league.Name = model.Name;
            
            await repo.SaveChangesAsync();
            return league.Id;
        }

        public async Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync()
        {
            var leagues = await repo.AllReadonly<League>()
                .Include(t => t.Teams)
                .Select(t => new LeagueViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Count = t.Teams.Count,
                    Teams = t.Teams.Select(lt => new Models.Teams.TeamsShortViewModel()
                    {
                        Id = lt.Id,
                        Name = lt.Name,
                        CoachName = lt.Coach != null ? $"{lt.Coach.FirstName} {lt.Coach.LastName}" : "No coach assigned"
                    })
                })
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return leagues;
        }

        public async Task<LeagueViewModel> GetByIdAsync(int leagueId)
        {
            var league = await repo.All<League>()
                .Where(t => t.Id == leagueId)
                .Include(t => t.Teams)
                .Select(l => new LeagueViewModel()
                {
                    Id = l.Id,
                    Name = l.Name,
                    Count = l.Teams.Count
                })
                .FirstOrDefaultAsync();

            return league;
        }

        public async Task RemoveTeam(int teamId, int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (team != null && league != null && league.Teams.Any(t => t.Id == team.Id))
            league.Teams.Remove(team);

            await repo.SaveChangesAsync();
        }
    }
}
