using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachDetailsModel : CoachViewModel
    {
        public string? Team { get; set; }

        public int? TeamId { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>(); 
    }
}
