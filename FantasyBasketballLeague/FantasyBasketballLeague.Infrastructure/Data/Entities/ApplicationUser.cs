using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FantasyBasketballLeague.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Logged in user list of teams.
        /// </summary>
        public virtual ICollection<UserTeam> UserTeams { get; set; } = new HashSet<UserTeam>();
    }
}
