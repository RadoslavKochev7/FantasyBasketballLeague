using FantasyBasketballLeague.Core.Models.Coach;
using FantasyBasketballLeague.Core.Models.League;
using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Core.Models.Teams
{
    public class TeamAddModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; } = null!;

        public int? CoachId { get; set; }

        [StringLength(FirstNameMaxLength, MinimumLength = LastNameMinLength)]
        public string? Coach { get; set; }

        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength)]
        public string? League { get; set; }

        public int? LeagueId { get; set; }

        [Required]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; } = null!;

        public IEnumerable<LeagueViewModel> Leagues { get; set; } = new List<LeagueViewModel>();

        public IEnumerable<CoachViewModel> Coaches { get; set; } = new List<CoachViewModel>();
    }
}
