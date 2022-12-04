using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachAssignToTeamModel
    {
        public int TeamId { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
    }
}
