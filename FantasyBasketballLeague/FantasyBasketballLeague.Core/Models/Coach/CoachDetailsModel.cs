using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachDetailsModel : CoachViewModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>(); 
    }
}
