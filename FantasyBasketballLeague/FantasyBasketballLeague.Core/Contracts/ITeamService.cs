using FantasyBasketballLeague.Core.Models;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();
    }
}
