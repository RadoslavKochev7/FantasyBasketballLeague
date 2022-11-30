using FantasyBasketballLeague.Core.Models.BasketballPlayer;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface IPlayerService
    {
        Task<int> AddAsync(BasketballPlayerViewModel model);
        Task<int> Edit(int id, BasketballPlayerDetailsModel model);
        Task DeleteAsync(int id);
        Task<BasketballPlayerDetailsModel> GetByIdAsync(int id);
        Task<bool> PlayerNameExists(string playerName, string lastName);
        Task<IEnumerable<BasketballPlayerDetailsModel>> GetAllPlayersAsync();
    }
}
