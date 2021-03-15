using System.Collections.Immutable;
using System;
using Microsoft.EntityFrameworkCore;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Persistence.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<LobbyGame> LobbyGames{get;set;}
        public DbSet<Mode> Modes{get;set;}
        public DbSet<Player> Players{get;set;}
        public DbSet<PlayerInfo> PlayersInfo{get;set;}
        public DbSet<PlayerLobbyGame> PlayerLobbyGames{get;set;}
        public DbSet<PlayerRole> PlayerRoles{get;set;}
        public DbSet<Role> Roles{get;set;}
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(x=>x.Id);
            modelBuilder.Entity<Role>().Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(x=>x.Name).IsRequired();
            modelBuilder.Entity<Role>().HasMany(x=>x.PlayerRoles).WithOne(x=>x.Role).HasForeignKey(z=>z.RoleId);
            modelBuilder.Entity<Role>().HasData(
                new Role{Id=1, Name="Player"},
                new Role{Id=2, Name="Admin"}
            );

            modelBuilder.Entity<Mode>().ToTable("Modes");
            modelBuilder.Entity<Mode>().HasKey(x=>x.Id);
            modelBuilder.Entity<Mode>().Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Mode>().Property(x=>x.ModeName).IsRequired();
            modelBuilder.Entity<Mode>().HasMany(x=>x.LobbyGames).WithOne(x=>x.Mode).HasForeignKey(z=>z.ModeId);
            modelBuilder.Entity<Mode>().HasData(
                new Mode{Id=1, ModeName="Deathmatch"},
                new Mode{Id=2, ModeName="Battle Royale"});
            
            modelBuilder.Entity<PlayerInfo>().ToTable("Players Info");
            modelBuilder.Entity<PlayerInfo>().Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<PlayerInfo>().HasOne(x=>x.Player).WithOne(y=>y.PlayerInfo).HasForeignKey<Player>(y=>y.PlayerInfoId);
            modelBuilder.Entity<PlayerInfo>().HasData(
                new PlayerInfo{Id=1,PassedGames = 0, MaxDamage = 0, MaxKills = 0}
            );

            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>().HasKey(x=>x.Id);
            modelBuilder.Entity<Player>().Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Player>().Property(x=>x.GameLogin).IsRequired();
            modelBuilder.Entity<Player>().Property(x=>x.Email).IsRequired();
            modelBuilder.Entity<Player>().Property(x=>x.Password).IsRequired();
            modelBuilder.Entity<Player>().HasOne(x=>x.PlayerInfo).WithOne(y=>y.Player);
            modelBuilder.Entity<Player>().HasMany(x=>x.PlayerRoles).WithOne(y=>y.Player).HasForeignKey(x=>x.PlayerId);
            modelBuilder.Entity<Player>().HasMany(x=>x.PlayerLobbyGames).WithOne(y=>y.Player).HasForeignKey(x=>x.PlayerId);
            modelBuilder.Entity<Player>().HasData(
                new Player{
                    Id=1,
                    Login="supper24",
                    GameLogin="superman",
                    Email="superemail@gmail.com",
                    Password= Helpers.HelperMD5.GenerateMD5Hash("123123"),
                    PlayerInfoId =1
                }
            );

            modelBuilder.Entity<LobbyGame>().ToTable("LobbyGames");
            modelBuilder.Entity<LobbyGame>().HasKey(x=>x.Id);
            modelBuilder.Entity<LobbyGame>().Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<LobbyGame>().HasOne(x=>x.Mode).WithMany(x=>x.LobbyGames).HasForeignKey(x=>x.ModeId);
            modelBuilder.Entity<LobbyGame>().HasMany(x=>x.PlayerLobbyGames).WithOne(y=>y.LobbyGame).HasForeignKey(z=>z.LobbyGameId);

            modelBuilder.Entity<PlayerRole>().ToTable("PlayerRoles");
            modelBuilder.Entity<PlayerRole>().HasKey(x=>new{x.PlayerId,x.RoleId});
            modelBuilder.Entity<PlayerRole>().HasOne(x=>x.Role).WithMany(x=>x.PlayerRoles).HasForeignKey(x=>x.RoleId);
            modelBuilder.Entity<PlayerRole>().HasOne(x=>x.Player).WithMany(x=>x.PlayerRoles).HasForeignKey(x=>x.PlayerId);
            modelBuilder.Entity<PlayerRole>().HasData(
                new PlayerRole{
                    PlayerId = 1,
                    RoleId = 1
                },
                new PlayerRole{
                    PlayerId = 1,
                    RoleId = 2
                }
            );

            modelBuilder.Entity<PlayerLobbyGame>().ToTable("PlayersLobbyGames");
            modelBuilder.Entity<PlayerLobbyGame>().HasKey(x=>new{x.PlayerId,x.LobbyGameId});
            modelBuilder.Entity<PlayerLobbyGame>().HasOne(x=>x.LobbyGame).WithMany(x=>x.PlayerLobbyGames).HasForeignKey(x=>x.LobbyGameId);;
            modelBuilder.Entity<PlayerLobbyGame>().HasOne(x=>x.Player).WithMany(x=>x.PlayerLobbyGames).HasForeignKey(x=>x.PlayerId);
        }
    }
}