using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.BasketballPlayer
{
#nullable disable
    public class BasketballPlayerViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        public int PositionId { get; set; }

        [Required]
        [Display(Name = "Is Team Captain")]
        public string IsTeamCaptain { get; set; }

        [Required]
        [Display(Name = "Is Starter")]
        public string IsStarter { get; set; }

        [Required]
        [StringLength (JerseyMaxNumer, MinimumLength = JerseyMinNumer)]   
        public string JerseyNumber { get; set; } 
    }
}