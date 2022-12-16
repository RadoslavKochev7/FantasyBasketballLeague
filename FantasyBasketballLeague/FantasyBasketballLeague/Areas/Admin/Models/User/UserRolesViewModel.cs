using System.ComponentModel.DataAnnotations;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Areas.Admin.Models.User
{
    public class UserRolesViewModel
    {
        [Key]
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; } = null!;

        public string[] RoleNames { get; set; } = null!;
    }
}
