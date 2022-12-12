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
                throw new ArgumentNullException("Model cannot be null ");

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Initials))
                throw new InvalidDataException("Name/Initials cannot be null or empty");

            var position = new Position()
            {
                Name = model.Name,
                Initials = model.Initials
            };

            await repo.AddAsync(position);
            await repo.SaveChangesAsync();

            return position.Id;
        }

        public async Task DeleteAsync(int id)
        {
            if (!await repo.AllReadonly<Position>().AnyAsync(p => p.Id == id))
            {
                throw new InvalidOperationException();
            }

            await repo.DeleteAsync<Position>(id);
            await repo.SaveChangesAsync();
        }

        public async Task<int> Edit(int id, PositionViewModel model)
        {
            try
            {
                var position = await repo.GetByIdAsync<Position>(id);
                if (model == null)
                    throw new ArgumentNullException("Model cannot be null");

                position.Name = model.Name;
                position.Initials = model.Initials;

                await repo.SaveChangesAsync();
                return position.Id;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("No position with current Id");
            }
            catch (ArgumentNullException ane)
            {
                throw new ArgumentNullException(ane.Message);
            }
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
        {
            return await repo.All<Position>(t => t.Id == id)
               .Select(l => new PositionViewModel()
               {
                   Id = l.Id,
                   Name = l.Name,
                   Initials = l.Initials,
               })
               .FirstAsync() ?? throw new InvalidOperationException();
        }
    }
}
