using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
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
            if (string.IsNullOrEmpty(model.Name))
                throw new InvalidDataException("Name cannot be null or empty");


            if (model == null)
                throw new NullReferenceException();

            var league = new League()
            {
                Name = model.Name
            };

            await repo.AddAsync(league);
            await repo.SaveChangesAsync();

            return league.Id;
        }

        public async Task<int> AddTeams(int[] teamIds, int leagueId)
        {
            var countAddedTeams = 0;
            var league = await repo.GetByIdAsync<League>(leagueId);

            if (league == null)
            {
                return 0;
            }

            var teams = new List<Team>();
            foreach (var id in teamIds)
            {
                var team = await repo.GetByIdAsync<Team>(id);
                teams.Add(team);
            }

            league.Teams.ToList().AddRange(teams);
            return countAddedTeams;
        }

        public async Task DeleteAsync(int leagueId)
        {
            if (await repo.AllReadonly<League>().AnyAsync(l => l.Id == leagueId) == false)
                throw new ArgumentNullException($"No league with id {leagueId}");

            await repo.DeleteAsync<League>(leagueId);
            await repo.SaveChangesAsync();
        }

        public async Task<int> Edit(int leagueId, LeagueViewModel model)
        {
            var league = await repo.GetByIdAsync<League>(leagueId)
                ?? throw new ArgumentNullException($"No league with id {leagueId}");

            if (league.Id == model.Id)
                league.Name = model.Name;

            await repo.SaveChangesAsync();
            return league.Id;
        }

        public async Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync()
        {
            var leagues = await repo.AllReadonly<League>()
                .Include(t => t.Teams)
                .ThenInclude(t => t.Coach)
                .Select(t => new LeagueViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Count = t.Teams.Count,
                })
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return leagues;
        }


        public async Task<LeagueViewModel> GetByIdAsync(int leagueId)
        {
            return await repo.All<League>()
                 .Where(t => t.Id == leagueId)
                 .Include(t => t.Teams)
                 .Select(l => new LeagueViewModel()
                 {
                     Id = l.Id,
                     Name = l.Name,
                     Count = l.Teams.Count,
                     Teams = l.Teams.Select(lt => new Models.Teams.TeamsShortViewModel()
                     {
                         Id = lt.Id,
                         Name = lt.Name,
                         CoachName = lt.Coach != null ? $"{lt.Coach.FirstName} {lt.Coach.LastName}" : "No coach assigned",
                         LogoUrl = lt.LogoUrl,
                     })

                 })
                 .FirstAsync() ?? throw new InvalidOperationException();
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
