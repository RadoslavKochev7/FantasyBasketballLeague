using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using FantasyBasketballLeague.Core.Models.Teams;
using FantasyBasketballLeague.Infrastructure.Data.Common;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                IsStarter = model.IsStarter == "true",
                IsTeamCaptain = model.IsTeamCaptain == "true",
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
                player.IsActive = false;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<int> Edit(int id, BasketballPlayerDetailsModel model)
        {
            var player = await repo.GetByIdAsync<BasketballPlayer>(id);

            if (id == model.Id)
            {
                player.FirstName = model.FirstName;
                player.LastName = model.LastName;
                player.JerseyNumber = model.JerseyNumber;
                player.SeasonsPlayed = model.SeasonsPlayed;
                player.IsTeamCaptain = model.IsTeamCaptain == "true";
                player.IsStarter = model.IsStarter == "true";
                player.PositionId = model.PositionId;
                player.TeamId = model.TeamId;
            }

            await repo.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<BasketballPlayerDetailsModel>> GetAllPlayersAsync()
        {
            var model = await repo.AllReadonly<BasketballPlayer>()
                 .Where(x => x.IsActive)
                 .Include(t => t.Team)
                 .Include(p => p.Position)
                 .Select(p => new BasketballPlayerDetailsModel()
                 {
                     Id = p.Id,
                     FirstName = p.FirstName,
                     LastName = p.LastName,
                     SeasonsPlayed = p.SeasonsPlayed,
                     TeamId = p.TeamId,
                     Team = p.Team.Name,
                     PositionId = p.PositionId,
                     Position = p.Position.Name,
                     JerseyNumber = p.JerseyNumber,
                     IsStarter = p.IsStarter.HasValue && p.IsStarter.Value == true ? "Yes" : "No",
                     IsTeamCaptain = p.IsTeamCaptain.HasValue && p.IsTeamCaptain.Value == true ? "Yes" : "No",
                     Experience = p.ExperienceLevel.ToString()
                 })
                   .OrderByDescending(t => t.Id)
                   .ToListAsync();

            return model;
        }

        public Task<IEnumerable<MyPlayersModel>> GetMyPlayers()
        {
            //var user = await repo.All<ApplicationUser>()
            //   .Where(u => u.Id == userId)
            //   .Include(u => u.UserTeams)
            //   .ThenInclude(t => t.Team)
            //   .ThenInclude(t => t.Players)
            //   .ThenInclude(t => t.Position)
            //   .FirstOrDefaultAsync();

            //if (user == null)
            //{
            //    throw new ArgumentException("Invalid User Id");
            //}
            //var result = user.UserTeams
            //     .Select(u => new MyTeamViewModel()
            //     {
            //         Players = u.Team.Players.Select(p => new Models.BasketballPlayer.BasketballPlayerDetailsModel()
            //         {
            //             Id = p.Id,
            //             FirstName = p.FirstName,
            //             LastName = p.LastName,
            //             JerseyNumber = p.JerseyNumber,
            //             IsTeamCaptain = p.IsTeamCaptain == false ? "No" : "Yes",
            //             IsStarter = p.IsStarter == false ? "No" : "Yes",
            //             Position = p.Position.Initials,
            //             PositionId = p.PositionId,
            //             SeasonsPlayed = p.SeasonsPlayed
            //         })
            //         .OrderBy(p => p.JerseyNumber)
            //         .ToList()
            //     })
            //     .OrderByDescending(t => t.Id);

            //return result;

            throw new NotImplementedException();
        }

        public async Task<BasketballPlayerDetailsModel> GetByIdAsync(int id)
        {
            return await repo.All<BasketballPlayer>(x => x.Id == id)
               .Include(t => t.Team)
               .Include(p => p.Position)
               .Select(p => new BasketballPlayerDetailsModel()
               {
                   Id = id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   SeasonsPlayed = p.SeasonsPlayed,
                   TeamId = p.TeamId,
                   Team = p.Team.Name,
                   Position = p.Position.Name,
                   PositionId = p.PositionId,
                   JerseyNumber = p.JerseyNumber,
                   IsStarter = p.IsStarter == true ? "Yes" : "No",
                   IsTeamCaptain = p.IsTeamCaptain == true ? "Yes" : "No",
                   Experience = p.ExperienceLevel.ToString()
               })
               .FirstOrDefaultAsync() ?? throw new ArgumentNullException("No such player.");
        }


        public async Task<bool> PlayerNameExists(string firstName, string lastName)
        => await repo.AllReadonly<BasketballPlayer>().AnyAsync(t => t.FirstName == firstName
                                                                 && t.LastName == lastName);
    }
}
