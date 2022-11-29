using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.Position;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
    public class PositionService : IPositionService
    {
        private readonly IRepository repo;

        public PositionService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(PositionViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException();
            }

            var position = new Position()
            {
                Id = model.Id,
                Name = model.Name,
                Initials = model.Initials
            };

            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

            return position.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var position = await repo.GetByIdAsync<Position>(id);

            if (position != null)
            {
                await repo.DeleteAsync<Position>(id);
                await repo.SaveChangesAsync();
            }
        }

        public async Task<int> Edit(int id, PositionViewModel model)
        {
            var position = await repo.GetByIdAsync<Position>(id);

            if (id == model.Id)
            {
                position.Name = model.Name;
                position.Initials = model.Initials;
            }

            await repo.SaveChangesAsync();
            return position.Id;
        }

        public Task<bool> ExistsByName(string positionName)
         => repo.AllReadonly<Position>().AnyAsync(p => p.Name == positionName);

        public async Task<IEnumerable<PositionViewModel>> GetAllPositionsAsync()
        {
            return await repo.AllReadonly<Position>()
               .Select(c => new PositionViewModel()
               {
                   Id = c.Id,
                   Name = c.Name,
                   Initials = c.Initials,
               })
               .OrderByDescending(t => t.Id)
               .ToListAsync();
        }

        public async Task<PositionViewModel> GetByIdAsync(int id)
           => await repo.GetByIdAsync<PositionViewModel>(id);
    }
}
