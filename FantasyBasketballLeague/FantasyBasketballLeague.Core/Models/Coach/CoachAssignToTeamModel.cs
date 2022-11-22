using FantasyBasketballLeague.Core.Models.League;
using FantasyBasketballLeague.Core.Models.Teams;
using System.ComponentModel.DataAnnotations;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachAssignToTeamModel
    {
        [Required]
        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
    }
}
