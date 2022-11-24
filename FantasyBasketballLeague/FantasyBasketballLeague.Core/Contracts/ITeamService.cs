using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();
        Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync();
        Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync();
        Task<IEnumerable<MyTeamViewModel>> GetMyTeams(string userId);
        Task AddAsync(TeamAddModel model);
        Task<bool> TeamExists(string teamName);
        Task<int> Edit(int teamId, TeamViewModel model);
        Task DeleteAsync(int teamId);
        Task GetByIdAsync(int teamId);
    }
}
