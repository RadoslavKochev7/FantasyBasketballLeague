﻿// <auto-generated />
using System;
using FantasyBasketballLeague.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FantasyBasketballLeague.Infrastructure.Migrations
{
    [DbContext(typeof(FantasyLeagueDbContext))]
    partial class FantasyLeagueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FantasyBasketballLeague.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "05a1e706-e884-447c-8152-6f67231e2e10",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c469a294-dede-4369-b3a3-11e924c377bd",
                            Email = "player12@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "PLAYER12.COM",
                            NormalizedUserName = "PLAYER12",
                            PasswordHash = "AQAAAAEAACcQAAAAEOJDW/4wlC8dCen+lmYZPDcjlVASjLTBV+/Kkjm+vWLBTBjJMNL6SJrXUfmuExq12A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "47d15095-0263-4743-9207-73fa59b5838b",
                            TwoFactorEnabled = false,
                            UserName = "player12"
                        },
                        new
                        {
                            Id = "7e170021-8670-45dc-8352-67d285dbd759",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "07b5d297-44c9-4c82-9ca9-6056e31e819f",
                            Email = "guest@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@MAIL.COM",
                            NormalizedUserName = "GUEST123",
                            PasswordHash = "AQAAAAEAACcQAAAAEC7z6aSRc3+lTI2YKACRJJsrag8NqIpXVuCm+EjwTBQ+VZEglbJJc6svuI8WbMnsSQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2cd03d70-3aa9-47a9-9914-a8907b7d41ca",
                            TwoFactorEnabled = false,
                            UserName = "guest123"
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.BasketballPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsStarter")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsTeamCaptain")
                        .HasColumnType("bit");

                    b.Property<string>("JerseyNumber")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<byte>("SeasonsPlayed")
                        .HasColumnType("tinyint");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.ToTable("BasketballPlayers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Radoslav",
                            IsStarter = true,
                            IsTeamCaptain = true,
                            JerseyNumber = "7",
                            LastName = "Kochev",
                            PositionId = 2,
                            SeasonsPlayed = (byte)12,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "LeBron",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "6",
                            LastName = "James",
                            PositionId = 3,
                            SeasonsPlayed = (byte)18,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Magic",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "32",
                            LastName = "Johnson",
                            PositionId = 1,
                            SeasonsPlayed = (byte)14,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Alexander",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "14",
                            LastName = "Vezenkov",
                            PositionId = 4,
                            SeasonsPlayed = (byte)9,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Nikola",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "15",
                            LastName = "Jokic",
                            PositionId = 5,
                            SeasonsPlayed = (byte)9,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Michael",
                            IsStarter = true,
                            IsTeamCaptain = true,
                            JerseyNumber = "23",
                            LastName = "Jordan",
                            PositionId = 2,
                            SeasonsPlayed = (byte)16,
                            TeamId = 2
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Luca",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "77",
                            LastName = "Doncic",
                            PositionId = 1,
                            SeasonsPlayed = (byte)5,
                            TeamId = 2
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Dirk",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "41",
                            LastName = "Nowitzki",
                            PositionId = 4,
                            SeasonsPlayed = (byte)20,
                            TeamId = 2
                        },
                        new
                        {
                            Id = 9,
                            FirstName = "Todor",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "10",
                            LastName = "Stoykov",
                            PositionId = 3,
                            SeasonsPlayed = (byte)16,
                            TeamId = 2
                        },
                        new
                        {
                            Id = 10,
                            FirstName = "Shaquille",
                            IsStarter = true,
                            IsTeamCaptain = false,
                            JerseyNumber = "34",
                            LastName = "O'Neal",
                            PositionId = 5,
                            SeasonsPlayed = (byte)13,
                            TeamId = 2
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.ToTable("Coaches");

                    b.HasData(
                        new
                        {
                            Id = 12,
                            FirstName = "Mike",
                            ImageUrl = "https://cdn.britannica.com/72/212572-050-12A50228/Duke-University-mens-basketball-Mike-Krzyzewski.jpg",
                            LastName = "Krzyzewski",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 13,
                            FirstName = "Phil",
                            ImageUrl = "https://cdn.britannica.com/38/219738-050-6AC916F7/American-basketball-player-and-coach-Phil-Jackson-2010.jpg",
                            LastName = "Jackson",
                            TeamId = 2
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fantasy League"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Amateur League"
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Initials = "PG",
                            Name = "Point Guard"
                        },
                        new
                        {
                            Id = 2,
                            Initials = "SG",
                            Name = "Shooting Guard"
                        },
                        new
                        {
                            Id = 3,
                            Initials = "SF",
                            Name = "Small Forward"
                        },
                        new
                        {
                            Id = 4,
                            Initials = "PF",
                            Name = "Power Forward"
                        },
                        new
                        {
                            Id = 5,
                            Initials = "C",
                            Name = "Center"
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OpenPositions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoachId = 2,
                            LeagueId = 1,
                            LogoUrl = "https://basketball.bg/pictures/pic_big/teams/20210929013457.png",
                            Name = "Dream Team",
                            OpenPositions = 10
                        },
                        new
                        {
                            Id = 2,
                            CoachId = 1,
                            LeagueId = 1,
                            LogoUrl = "https://pbs.twimg.com/profile_images/1214219629398286337/sgx5t_Qj_400x400.jpg",
                            Name = "The Lions",
                            OpenPositions = 10
                        });
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.UserTeam", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserTeams");

                    b.HasData(
                        new
                        {
                            UserId = "05a1e706-e884-447c-8152-6f67231e2e10",
                            TeamId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.BasketballPlayer", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Infrastructure.Data.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Coach", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", "Team")
                        .WithOne("Coach")
                        .HasForeignKey("FantasyBasketballLeague.Infrastructure.Data.Entities.Coach", "TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Infrastructure.Data.Entities.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.UserTeam", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FantasyBasketballLeague.Data.Entities.ApplicationUser", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FantasyBasketballLeague.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FantasyBasketballLeague.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FantasyBasketballLeague.Data.Entities.ApplicationUser", b =>
                {
                    b.Navigation("UserTeams");
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.League", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("FantasyBasketballLeague.Infrastructure.Data.Entities.Team", b =>
                {
                    b.Navigation("Coach");

                    b.Navigation("Players");

                    b.Navigation("TeamUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
