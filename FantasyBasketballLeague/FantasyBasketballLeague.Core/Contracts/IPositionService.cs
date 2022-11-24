using FantasyBasketballLeague.Core.Models.Position;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionViewModel>> GetAllPositionsAsync();
    }
}
