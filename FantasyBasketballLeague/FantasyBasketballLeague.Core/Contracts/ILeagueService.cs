using FantasyBasketballLeague.Core.Models.League;

namespace FantasyBasketballLeague.Core.Contracts
{
    public interface ILeagueService
    {
        Task AddAsync(LeagueViewModel model);
        Task Edit(int leagueId, LeagueViewModel model);
        Task DeleteAsync(int leagueId);
        Task GetByIdAsync(int leagueId);
        Task AddTeam(int teamId, int leagueId);
        Task RemoveTeam(int teamId, int leagueId);
    }
}
