using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Core.Models.Teams
{
    public class TeamsShortViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; } = null!;

        [Display(Name = "Coach Name")]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string? CoachName { get; set; }

        [Required]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; } = null!;
    }
}
