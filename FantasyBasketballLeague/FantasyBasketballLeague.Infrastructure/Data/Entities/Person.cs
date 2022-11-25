using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        [MinLength(FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        [MinLength(LastNameMinLength)]
        public string LastName { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
