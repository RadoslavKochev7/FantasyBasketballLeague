using Microsoft.AspNetCore.Identity;

namespace FantasyBasketballLeague.Infrastructure.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Logged in user list of teams.
        /// </summary>
        public virtual ICollection<UserTeam> UserTeams { get; set; } = new HashSet<UserTeam>();
    }
}
