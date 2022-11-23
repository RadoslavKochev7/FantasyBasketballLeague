using FantasyBasketballLeague.Core.Models.BasketballPlayer;
using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.Teams
{

    public class TeamViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Open Positions")]
        public int OpenPositions { get; set; } = OpenPositionsDefaultValue;

        [Display(Name = "Coach Name")]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string? CoachName { get; set; }

        public int? CoachId { get; set; }

        [StringLength (LeagueNameMaxLength, MinimumLength = LeagueNameMinLength)]   
        public string? League { get; set; }

        public int? LeagueId { get; set; }

        [Required]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; } = null!;

    }
}
