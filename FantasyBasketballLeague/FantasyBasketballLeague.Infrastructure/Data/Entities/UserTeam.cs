using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    /// <summary>
    /// Mapping table Many-to-many relation of teams and users.
    /// </summary>
    public class UserTeam
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int TeamId { get; set; }


        [ForeignKey(nameof(TeamId))]
        [Required]
        public virtual Team Team { get; set; } = null!;
    }
}
