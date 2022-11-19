using FantasyBasketballLeague.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachDetailsModel : Person
    {
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public string? Team { get; set; }

        public int? TeamId { get; set; }
    }
}
