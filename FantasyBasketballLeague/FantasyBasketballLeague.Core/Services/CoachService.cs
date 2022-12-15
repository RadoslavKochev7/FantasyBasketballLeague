using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
#nullable disable

    public class CoachService : ICoachService
    {
        private readonly IRepository repo;

        public CoachService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(CoachViewModel model)
        {
            if (model is null)
                throw new NullReferenceException("Model cannot be null");

            var coach = new Coach()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl
            };

            try
            {
                await repo.AddAsync(coach);
                await repo.SaveChangesAsync();
                return coach.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        public async Task DeleteAsync(int coachId)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId)
                ?? throw new InvalidOperationException("Coach cannot be null");

            if (coach != null)
            {
                coach.IsActive = false;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<int> Edit(int coachId, CoachDetailsModel model)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId)
               ?? throw new InvalidOperationException("Coach cannot be null");

            if (coachId == model.Id)
            {
                coach.FirstName = model.FirstName;
                coach.LastName = model.LastName;
                coach.ImageUrl = model.ImageUrl;
                coach.TeamId = model.TeamId;
            }

            await repo.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<CoachDetailsModel>> GetAllCoachesAsync()
        {
            return await repo.AllReadonly<Coach>()
                .Where(c => c.IsActive)
                .Include(t => t.Team)
                .Select(c => new CoachDetailsModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ImageUrl = c.ImageUrl,
                    TeamId = c.TeamId ?? 0,
                    Team = c.TeamId != 0 ? c.Team.Name : "No team assigned"
                })
                .OrderByDescending(t => t.Id)
                .ToListAsync();
        }

        public async Task<CoachDetailsModel> GetByIdAsync(int coachId)
        {
            return await repo.All<Coach>(x => x.Id == coachId)
                .Where(x => x.IsActive)
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
                .FirstAsync() ?? throw new InvalidOperationException("Coach cannot be null");
        }

        public async Task<IEnumerable<CoachDetailsModel>> AvailableCoaches()
        {
            return await repo.AllReadonly<Coach>(c => c.IsActive)
                .Where(c => c.TeamId == null)
                .Select(c => new CoachDetailsModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                })
                .ToListAsync();
        }

        public async Task<bool> AssignToTeam(int coachId, int teamId)
        {
            var coach = await repo.GetByIdAsync<Coach>(coachId);
            var team = await repo.GetByIdAsync<Team>(teamId);
            bool isAdded = false;

            if (coach != null && team != null)
            {
                if (team.CoachId == null)
                {
                    coach.TeamId = teamId;
                    team.CoachId = coach.Id;
                    isAdded = true;
                    await repo.SaveChangesAsync();
                }
            }

            return isAdded;
        }
    }
}
