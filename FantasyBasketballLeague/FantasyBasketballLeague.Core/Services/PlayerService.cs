using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository repo;

        public PlayerService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(BasketballPlayerViewModel model)
        {
            var player = new BasketballPlayer()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                SeasonsPlayed = model.SeasonsPlayed,
                TeamId = model.TeamId,
                IsStarter = model.IsStarter == "Yes" ? true : false,
                IsTeamCaptain = model.IsTeamCaptain == "Yes" ? true : false,
                JerseyNumber = model.JerseyNumber,
                PositionId = model.PositionId,
            };

            await repo.AddAsync(player);
            await repo.SaveChangesAsync();

            return player.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var player = await repo.GetByIdAsync<BasketballPlayer>(id);

            if (player != null)
            {
                //player.IsActive = false;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<int> Edit(int id, BasketballPlayerViewModel model)
        {
            var player = await repo.GetByIdAsync<BasketballPlayer>(id);

            if (id == model.Id)
            {
                player.FirstName = model.FirstName;
                player.LastName = model.LastName;
                player.JerseyNumber = model.JerseyNumber;
                player.SeasonsPlayed = model.SeasonsPlayed;
                player.IsTeamCaptain = model.IsTeamCaptain == "Yes";
                player.IsStarter = model.IsStarter == "Yes";


            }

            await repo.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<BasketballPlayerViewModel>> GetAllPlayersAsync()
        {
            return await repo.AllReadonly<BasketballPlayer>()
                 .Include(t => t.Team)
                 .Include(p => p.Position)
                 .Select(p => new BasketballPlayerViewModel()
                 {
                     Id = p.Id,
                     FirstName = p.FirstName,
                     LastName = p.LastName,
                     SeasonsPlayed = p.SeasonsPlayed,
                     TeamId = p.TeamId,
                     Team = p.Team.Name,
                     PositionId = p.PositionId,
                     Position = p.Position.Name,
                     IsStarter = p.IsStarter.HasValue == true ? "Yes" : "No",
                     IsTeamCaptain = p.IsTeamCaptain.HasValue == true ? "Yes" : "No",

                 })
                   .OrderByDescending(t => t.Id)
                   .ToListAsync();
        }

        public async Task<BasketballPlayerViewModel> GetByIdAsync(int id)
        {
            var model = await repo.All<BasketballPlayer>()
               .Where(x => x.Id == id)
               .Include(t => t.Team)
               .Include(p => p.Position)
               .Select(p => new BasketballPlayerViewModel()
               {
                   Id = id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   SeasonsPlayed = p.SeasonsPlayed,
                   TeamId = p.TeamId,
                   Team = p.Team.Name,
                   PositionId = p.PositionId,
                   Position = p.Position.Name,
                   IsStarter = p.IsStarter.HasValue == true ? "Yes" : "No",
                   IsTeamCaptain = p.IsTeamCaptain.HasValue == true ? "Yes" : "No",

               })
               .FirstOrDefaultAsync();

            return model;
        }

        public async Task<bool> PlayerNameExists(string firstName, string lastName)
        => await repo.AllReadonly<BasketballPlayer>().AnyAsync(t => t.FirstName == firstName
                                                                 && t.LastName == lastName);
    }
}
