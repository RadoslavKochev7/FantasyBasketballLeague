using FantasyBasketballLeague.Core.Models.Coach;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ICoachService
    {
        Task<int> AddAsync(CoachViewModel model);
        Task<int> Edit (int coachId, CoachDetailsModel model);
        Task DeleteAsync(int coachId);
        Task<CoachDetailsModel> GetByIdAsync(int coachId);
        Task<IEnumerable<CoachDetailsModel>> GetAllCoachesAsync();
        Task<IEnumerable<CoachDetailsModel>> AvailableCoaches();
    }
}
