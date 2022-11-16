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
        Task AddAsync(TeamAddModel model);
        Task<bool> TeamExists(int teamId);
    }
}
