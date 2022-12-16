using FantasyBasketballLeague.Areas.Admin.Constants;
using System.ComponentModel.DataAnnotations;

namespace FantasyBasketballLeague.Areas.Admin.Models.User
{
    public class IdentityRoleCreateModel
    {
        [Required]
        [StringLength(AdminConstants.RoleNameMaxLength, MinimumLength = AdminConstants.RoleNameMinLength)]
        public string Name { get; set; } = null!;
    }
}
