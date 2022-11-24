using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.Position
{
    public class PositionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(InitialsMaxLength, MinimumLength = InitialsMinLength)]
        public string Initials { get; set; } = null!;
    }
}
