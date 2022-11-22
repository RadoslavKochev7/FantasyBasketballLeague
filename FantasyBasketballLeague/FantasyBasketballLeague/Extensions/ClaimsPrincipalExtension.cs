using System.Security.Claims;

namespace FantasyBasketballLeague.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string UserId(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
