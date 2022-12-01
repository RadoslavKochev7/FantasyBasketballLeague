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

            team.Coach = teamCoach ?? null;
            team.League = teamLeague ?? null;

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int teamId)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (team != null)
            {
                team.IsActive = false;
            }

            await repo.SaveChangesAsync();
        }

        public async Task<int> Edit(int teamId, TeamAddModel model)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);
            var teamCoach = await repo.GetByIdAsync<Coach>(model.CoachId ?? 0);
            var teamLeague = await repo.GetByIdAsync<League>(model.LeagueId ?? 0);

            if (teamCoach == null && team.CoachId != 0)
            {
                var coach = await repo.GetByIdAsync<Coach>(team.CoachId ?? 0);
                coach.TeamId = null;
                coach.Team = null;
            }
           
            team.Name = model.Name;
            team.LogoUrl = model.LogoUrl;
            team.LeagueId = model.LeagueId ?? null;
            team.CoachId = model.CoachId ?? null;
            team.Coach = teamCoach;
            team.League = teamLeague;

            await repo.SaveChangesAsync();
            return teamId;
        }

        public async Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync()
        {
            return await repo.AllReadonly<Coach>()
                .Where(c => c.TeamId == null && c.IsActive)
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
            var teams = await repo.AllReadonly<Team>()
             .Where(t => t.IsActive)
             .Include(t => t.Coach)
             .Include(t => t.League)
             .Select(t => new TeamViewModel()
             {
                 Id = t.Id,
                 Name = t.Name,
                 League = t.League.Name,
                 LeagueId = t.LeagueId,
                 LogoUrl = t.LogoUrl,
                 CoachId = t.CoachId.HasValue ? t.CoachId : null,
                 CoachName = $"{t.Coach.FirstName} {t.Coach.LastName}" ?? "No coach assigned",
                 OpenPositions = t.OpenPositions - t.Players.Count()
             })
             .ToListAsync();

            return teams;
        }

        public async Task<IEnumerable<MyTeamViewModel>> GetMyTeams(string userId)
        {
            var user = await repo.All<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Include(u => u.UserTeams)
               .ThenInclude(t => t.Team)
               .ThenInclude(t => t.Players)
               .ThenInclude(t => t.Position)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User Id");
            }

            var result = user.UserTeams
                .Select(u => new MyTeamViewModel()
                {
                    Id = u.TeamId,
                    Name = u.Team.Name,
                    LogoUrl = u.Team.LogoUrl,
                    Players = u.Team.Players.Select(p => new Models.BasketballPlayer.BasketballPlayerDetailsModel()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        JerseyNumber = p.JerseyNumber,
                        IsTeamCaptain = p.IsTeamCaptain == false ? "No" : "Yes",
                        IsStarter = p.IsStarter == false ? "No" : "Yes",
                        Position = p.Position.Initials,
                        PositionId = p.PositionId,
                        SeasonsPlayed = p.SeasonsPlayed
                    })
                    .ToList()
                });

            return result;
        }

        public async Task<TeamViewModel> GetByIdAsync(int teamId)
        {
            var team = await repo.All<Team>()
                .Where(t => t.Id == teamId)
                .Include(c => c.Coach)
                .Include(l => l.League)
                .Include(bp => bp.Players)
                .Select(t => new TeamViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    League = t.League.Name ?? "No league assigned",
                    LeagueId = t.LeagueId,
                    LogoUrl = t.LogoUrl,
                    CoachId = t.CoachId.HasValue ? t.CoachId : null,
                    CoachName = $"{t.Coach.FirstName[0]}.{t.Coach.LastName}" ?? "No coach assigned",
                    OpenPositions = t.OpenPositions - t.Players.Count(),
                })
                .FirstOrDefaultAsync();

            return team;
        }

        public async Task<bool> TeamExists(string teamName)
        => await repo.AllReadonly<Team>().AnyAsync(t => t.Name == teamName);
    }
}
