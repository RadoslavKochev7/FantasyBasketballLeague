using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.Coach
{
#nullable disable
    public class CoachViewModel 
    {
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string TeamName { get; set; }

        public int? TeamId { get; set; }
    }
}
