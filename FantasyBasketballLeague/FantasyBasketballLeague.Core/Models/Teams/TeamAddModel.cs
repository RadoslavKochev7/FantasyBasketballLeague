using FantasyBasketballLeague.Infrastructure.Data.Entities;
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

        public int? LeagueId { get; set; }

        [Required]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; } = null!;
    }
}
