using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
    [Authorize]
    public class CoachService : ICoachService
    {
        private readonly IRepository repo;

        public CoachService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(CoachViewModel model)
        {
            var coach = new Coach()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl
            };

            await repo.AddAsync(coach);
            await repo.SaveChangesAsync();

            return coach.Id;
        }

        public async Task AddToTeam(int coachId, int teamId)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId);
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (coach != null && team != null)
            {
                team.Coach = coach;
                await repo.SaveChangesAsync();
            }

        }

        public async Task DeleteAsync(int coachId)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId);
            //coach.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task Edit(int coachId, CoachDetailsModel model)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId);

            if (coachId == model.Id)
            {
                coach.FirstName = model.FirstName;
                coach.LastName = model.LastName;
                coach.ImageUrl = model.ImageUrl;
                coach.TeamId = model.TeamId;
            }

            await repo.SaveChangesAsync();
        }

        public async Task<CoachDetailsModel> GetByIdAsync(int coachId)
        {
            var model = await repo.AllReadonly<Coach>()
                .Where(x => x.Id == coachId)
                .Include(t => t.Team)
                .Select(c => new CoachDetailsModel()
                {
                    Id = coachId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ImageUrl = c.ImageUrl,
                    TeamId = c.TeamId ?? 0,
                    Team = c.TeamId != 0 ? c.Team.Name : "No team assigned"
                })
                .FirstAsync();

            return model;
        }
         

        public async Task RemoveFromTeam(int teamId)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);

            if (team != null)
            {
                team.Coach = null;
                await repo.SaveChangesAsync();
            }
        }
    }
}
