using FantasyBasketballLeague.Core.Contracts;
using FantasyBasketballLeague.Core.Services;
using FantasyBasketballLeague.Infrastructure.Data.Common;

namespace FantasyBasketballLeague.Extensions
{
    public static class FantasyBasketballLeagueServiceCollection 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ILeagueService, LeagueService>();

            return services;
        }
    }
}
