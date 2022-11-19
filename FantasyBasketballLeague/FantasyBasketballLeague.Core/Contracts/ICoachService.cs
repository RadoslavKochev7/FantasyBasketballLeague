using FantasyBasketballLeague.Core.Models.Coach;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ICoachService
    {
        Task AddAsync(CoachViewModel model);
        Task Edit (int coachId, CoachDetailsModel model);
        Task DeleteAsync(int coachId);
    }
}
