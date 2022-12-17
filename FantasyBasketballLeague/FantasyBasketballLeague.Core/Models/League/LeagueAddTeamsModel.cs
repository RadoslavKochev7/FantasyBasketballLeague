using FantasyBasketballLeague.Core.Models.Teams;

namespace FantasyBasketballLeague.Core.Models.League
{
    public class LeagueAddTeamsModel
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = null!;

        public string[] TeamNames { get; set; } = null!;

        //public List<TeamsShortViewModel> Teams { get; set; } = new List<TeamsShortViewModel>();
    }
}
