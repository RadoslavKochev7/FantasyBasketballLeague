using System.ComponentModel.DataAnnotations;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    /// <summary>
    /// Player's position on the court.
    /// </summary>
    public class Position
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Position name.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Position initials. Shorter form of name.
        /// </summary>
        [Required]
        public string Initials { get; set; } = null!;
    }
}