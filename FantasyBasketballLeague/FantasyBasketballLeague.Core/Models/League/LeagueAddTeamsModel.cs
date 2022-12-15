using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.League
{
    public class LeagueAddTeamsModel
    {
        public int LeagueId { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; } = null!;

        public bool IsSelected { get; set; } = false;

        public List<TeamsShortViewModel> Teams { get; set; } = new List<TeamsShortViewModel>();
    }
}
