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
    }
}
