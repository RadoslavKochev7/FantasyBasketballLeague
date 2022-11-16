using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Core.Models.League
{
    public class LeagueViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength)]
        public string Name { get; set; } = null!;
    }
}
