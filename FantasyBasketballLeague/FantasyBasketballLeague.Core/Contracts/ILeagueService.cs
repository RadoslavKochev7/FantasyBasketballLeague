using FantasyBasketballLeague.Core.Models.League;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ILeagueService
    {
        Task<int> AddAsync(LeagueViewModel model);
        Task<int> Edit(int leagueId, LeagueViewModel model);
        Task DeleteAsync(int leagueId);
        Task<LeagueViewModel> GetByIdAsync(int leagueId);
        Task AddTeam(int teamId, int leagueId);
        Task RemoveTeam(int teamId, int leagueId);
        Task<IEnumerable<LeagueViewModel>> GetAllLeaguesAsync();
    }
}
