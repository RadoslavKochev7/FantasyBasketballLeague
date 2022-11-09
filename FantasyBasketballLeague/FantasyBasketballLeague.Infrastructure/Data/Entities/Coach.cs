using FantasyBasketballLeague.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    public class Coach : Person
    {
        /// <summary>
        /// Coach's team. Can be null.
        /// </summary>
        [ForeignKey(nameof(TeamId))]
        public virtual Team? Team { get; set; }

        /// <summary>
        /// Coach's team id.
        /// </summary>
        public int? TeamId { get; set; }

        /// <summary>
        /// Coach image.
        /// </summary>
        public string? ImageUrl { get; set; }

    }
}