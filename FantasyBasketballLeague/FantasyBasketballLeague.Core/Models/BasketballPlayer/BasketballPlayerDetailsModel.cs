using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.BasketballPlayer
{
#nullable disable
    public class BasketballPlayerDetailsModel : BasketballPlayerViewModel
    {
        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Position { get; set; }

        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Team { get; set; }

        public string Experience { get; set; }
    }
}