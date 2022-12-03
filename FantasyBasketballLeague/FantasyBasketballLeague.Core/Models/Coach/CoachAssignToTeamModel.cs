using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachAssignToTeamModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
    }
}
