using FantasyBasketballLeague.Core.Models.Team;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();
        Task AddAsync(TeamAddModel model);
        Task<bool> TeamExists(int teamId);
    }
}
