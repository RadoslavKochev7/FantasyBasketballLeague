using FantasyBasketballLeague.Core.Models.Coach;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ICoachService
    {
        Task AddAsync(CoachViewModel model);
    }
}
