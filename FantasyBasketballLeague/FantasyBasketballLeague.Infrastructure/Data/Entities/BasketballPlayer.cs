using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FantasyBasketballLeague.Infrastructure.Data.Enums;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    /// <summary>
    /// Basketball player entity.
    /// </summary>
    public class BasketballPlayer : Person
    {
        /// <summary>
        /// The number of player's jersey.
        /// </summary>
        [Required]
        [MaxLength(JerseyMaxNumer)]
        public string JerseyNumber { get; set; } = null!;

        /// <summary>
        /// Player's team id.
        /// </summary>
        [ForeignKey(nameof(TeamId))]
        [Required]
        public int TeamId { get; set; }

        /// <summary>
        /// Player's team.
        /// </summary>
        [Required]
        public virtual Team Team { get; set; } = null!;

        /// <summary>
        /// Player's position id.
        /// </summary>
        [Required]
        [ForeignKey(nameof(PositionId))]
        public int PositionId { get; set; }

        /// <summary>
        /// Player's position.
        /// </summary>
        [Required]
        public virtual Position Position { get; set; } = null!;

        /// <summary>
        /// Shows is the player captain or no. A team can have only one captain.
        /// </summary>
        public bool? IsTeamCaptain { get; set; }

        /// <summary>
        /// Boolean showing is the player present in team's starting five players.
        /// </summary>
        public bool? IsStarter { get; set; }

        /// <summary>
        /// The number of seasons the player has.
        /// </summary>
        [Required]
        public byte SeasonsPlayed { get; set; }

        /// <summary>
        /// Player's level of experience. Can be Rookie, Average or Veteran, depending from seasons played.
        /// </summary>
        public ExperienceLevelType ExperienceLevel
            => GetLevelOfExperience();


        private ExperienceLevelType GetLevelOfExperience()
        {
            if (SeasonsPlayed > 9)
            {
                return ExperienceLevelType.Veteran;
            }
            else if (SeasonsPlayed > 3)
            {
                return ExperienceLevelType.Average;
            }

            return ExperienceLevelType.Rookie;
        }

    }
}
