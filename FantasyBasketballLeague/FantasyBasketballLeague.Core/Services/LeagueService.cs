using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
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

        public async Task<int> AddAsync(LeagueViewModel model)
        {
            var league = new League()
            {
                Id = model.Id,
                Name = model.Name,
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
                await repo.SaveChangesAsync();

            //coach.IsActive = false;

        }

        public async Task<int> Edit(int leagueId, LeagueViewModel model)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            if (leagueId == model.Id)
                league.Name = model.Name;
            

            await repo.SaveChangesAsync();
            return league.Id;
        }

        public async Task GetByIdAsync(int leagueId)
        => await repo.GetByIdAsync<League>(leagueId);

        public async Task RemoveTeam(int teamId, int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (team != null && league != null) 
            league.Teams.Remove(team);

            await repo.SaveChangesAsync();
        }
    }
}
