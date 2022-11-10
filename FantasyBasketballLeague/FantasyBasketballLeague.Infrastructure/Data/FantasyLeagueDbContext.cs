using FantasyBasketballLeague.Infrastructure.Data.Configurations;
using FantasyBasketballLeague.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FantasyBasketballLeague.Infrastructure.Data;

public class FantasyLeagueDbContext : IdentityDbContext<ApplicationUser>
{
    public FantasyLeagueDbContext(DbContextOptions<FantasyLeagueDbContext> options)
        : base(options)
    {
    }

    public DbSet<BasketballPlayer> BasketballPlayers { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<Coach> Coaches { get; set; } = null!;
    public DbSet<UserTeam> UserTeams { get; set; } = null!;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

  
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserTeamConfiguration());
        builder.ApplyConfiguration(new CoachConfiguration());
        builder.ApplyConfiguration(new LeagueConfiguration());
        builder.ApplyConfiguration(new PositionConfiguration());
        builder.ApplyConfiguration(new TeamConfiguration());
        builder.ApplyConfiguration(new PlayerConfiguration());

        base.OnModelCreating(builder);
    }
}