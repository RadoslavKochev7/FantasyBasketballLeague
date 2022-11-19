using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;

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

        public async Task AddAsync(CoachViewModel model)
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
    }
}
