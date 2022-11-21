namespace FantasyBasketballLeague.Core.Models.BasketballPlayer
{
#nullable disable
    public class BasketballPlayerViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public int PositionId { get; set; }

        public string JerseyNumber { get; set; } = null!;
    }
}