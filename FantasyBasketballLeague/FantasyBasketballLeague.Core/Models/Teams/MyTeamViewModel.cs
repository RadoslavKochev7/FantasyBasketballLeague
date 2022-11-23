using FantasyBasketballLeague.Core.Models.BasketballPlayer;


namespace FantasyBasketballLeague.Core.Models.Teams
{

    public class MyTeamViewModel : TeamViewModel
    {
        public List<BasketballPlayerViewModel> Players { get; set; } = new List<BasketballPlayerViewModel>();

    }
}
