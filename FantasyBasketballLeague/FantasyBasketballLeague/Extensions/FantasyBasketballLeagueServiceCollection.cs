using AspNetCoreHero.ToastNotification;
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
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });

            return services;
        }
    }
}
