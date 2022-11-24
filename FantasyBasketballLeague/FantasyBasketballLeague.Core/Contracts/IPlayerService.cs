using FantasyBasketballLeague.Core.Models.BasketballPlayer;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface IPlayerService
    {
        Task<int> AddAsync(BasketballPlayerViewModel model);
        Task<int> Edit(int id, BasketballPlayerViewModel model);
        Task DeleteAsync(int id);
        Task<BasketballPlayerViewModel> GetByIdAsync(int id);
        Task<bool> PlayerNameExists(string playerName, string lastName);
        Task<IEnumerable<BasketballPlayerViewModel>> GetAllPlayersAsync();
    }
}
