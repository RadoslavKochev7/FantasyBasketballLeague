using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    /// <summary>
    /// Basketball team, made of Players and a Coach.
    /// </summary>
    public class Team
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Team name.
        /// </summary>
        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The available positions in a Team. By default they will be 10.
        /// </summary>
        [Required]
        public int OpenPositions { get; set; } = OpenPositionsDefaultValue;

        /// <summary>
        /// Team coach. Can be null.
        /// </summary>
        public virtual Coach? Coach { get; set; }

        /// <summary>
        /// Coach id.
        /// </summary>
        public int? CoachId { get; set; }

        /// <summary>
        /// Team's league. One team can only play in one league.
        /// </summary>
        [ForeignKey(nameof(LeagueId))]
        public virtual League? League { get; set; } = null!;

        /// <summary>
        /// League's id.
        /// </summary>
        public int? LeagueId { get; set; }

        /// <summary>
        /// Team logo.Enter URL link.
        /// </summary>
        [Required]
        public string LogoUrl { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        /// <summary>
        /// List of all players in the team.
        /// </summary>
        public virtual IEnumerable<BasketballPlayer> Players { get; set; } = new List<BasketballPlayer>();

        /// <summary>
        /// Collection of user teams.
        /// </summary>
        public virtual IEnumerable<UserTeam> TeamUsers { get; set; } = new List<UserTeam>();
    }
}