using FantasyBasketballLeague.Core.Models.Coach;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ICoachService
    {
        Task<int> AddAsync(CoachViewModel model);
        Task Edit (int coachId, CoachDetailsModel model);
        Task DeleteAsync(int coachId);
        Task<CoachDetailsModel> GetByIdAsync(int coachId);
        Task AddToTeam(int coachId, int teamId);
        Task RemoveFromTeam(int teamId);
    }
}
