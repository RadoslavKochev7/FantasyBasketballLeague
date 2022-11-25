using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.BasketballPlayer
{
#nullable disable
    public class BasketballPlayerDetailsModel : BasketballPlayerViewModel
    {
        [Required]
        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Position { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Team { get; set; }
    }
}