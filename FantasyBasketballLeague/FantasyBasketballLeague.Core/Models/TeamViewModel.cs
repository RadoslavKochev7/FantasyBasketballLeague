using FantasyBasketballLeague.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models
{
#nullable disable

    public class TeamViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; }

        [Required]
        public int OpenPositions { get; set; } = OpenPositionsDefaultValue;

        //public virtual Coach? Coach { get; set; }

        //public int? CoachId { get; set; }

        [Required]
        [ForeignKey(nameof(LeagueId))]
        public string League { get; set; } 

        public int LeagueId { get; set; }

        [Required]
        public string LogoUrl { get; set; }

       
        //public virtual IEnumerable<BasketballPlayer> Players { get; set; } = new List<BasketballPlayer>();

        //public virtual IEnumerable<UserTeam> TeamUsers { get; set; } = new List<UserTeam>();

    }
}
