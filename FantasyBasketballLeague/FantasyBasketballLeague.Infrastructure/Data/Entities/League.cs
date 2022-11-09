using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// League name.
        /// </summary>
        [Required]
        [MaxLength(LeagueNameMaxLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Collection of teams in the league.
        /// </summary>
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
