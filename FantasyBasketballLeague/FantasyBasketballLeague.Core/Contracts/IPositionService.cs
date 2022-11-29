using FantasyBasketballLeague.Core.Models.Position;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionViewModel>> GetAllPositionsAsync();
        Task<int> AddAsync(PositionViewModel model);
        Task<int> Edit(int id, PositionViewModel model);
        Task DeleteAsync(int id);
        Task<bool> ExistsByName(string positionName); 
        Task<PositionViewModel> GetByIdAsync(int id);  
    }
}
