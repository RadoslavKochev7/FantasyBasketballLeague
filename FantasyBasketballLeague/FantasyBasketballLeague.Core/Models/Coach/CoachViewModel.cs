using FantasyBasketballLeague.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FantasyBasketballLeague.Core.Models.Coach
{
    public class CoachViewModel : Person
    {
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
    }
}
