using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();
        Task<IEnumerable<TeamViewModel>> GetAllTeamsWithoutCoaches();
        Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync();
        Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync();
        Task<IEnumerable<MyTeamViewModel>> GetMyTeams(string userId);
        Task AddAsync(TeamAddModel model);
        Task<bool> TeamExists(string teamName);
        Task<int> Edit(int teamId, TeamAddModel model);
        Task DeleteAsync(int teamId);
        Task<TeamViewModel> GetByIdAsync(int teamId);
    }
}
