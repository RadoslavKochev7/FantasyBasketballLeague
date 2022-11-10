using System.ComponentModel.DataAnnotations;

using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;

namespace FantasyBasketballLeague.Models.User.Register
{
#nullable disable

    public class RegisterViewModel
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
