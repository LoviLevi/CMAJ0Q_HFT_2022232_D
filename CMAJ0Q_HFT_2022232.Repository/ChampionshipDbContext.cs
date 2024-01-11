using CMAJ0Q_HFT_2022232.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CMAJ0Q_HFT_2022232.Repository
{
    public class ChampionshipDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public ChampionshipDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    //.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\ChampionshipDatabase.mdf;Integrated Security=True");
                    .UseInMemoryDatabase("championshipDb");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(Player => Player.Team)
                .WithMany(Team => Team.Players)
                .HasForeignKey(Player => Player.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(Team => Team.Championship)
                .WithMany(Championship => Championship.Teams)
                .HasForeignKey(Team => Team.ChampionshipId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            Championship c0 = new Championship() { ChampionshipId = 1, Name = "Premier League", Location = "England" };

            Team t0 = new Team() { TeamId = 1, Name = "Manchester United", Nickname = "The Red Devils" };
            Team t1 = new Team() { TeamId = 2, Name = "Liverpool", Nickname = "The Reds" };
            Team t2 = new Team() { TeamId = 3, Name = "Arsenal", Nickname = "The Gunners" };

            Player p0 = new Player() { PlayerId = 1, Name = "Marcus Rashford", Age = 25, Position = "Winger", Nationality = "England" };
            Player p1 = new Player() { PlayerId = 2, Name = "Mohamed Salah", Age = 30, Position = "Winger", Nationality = "Egypt" };
            Player p2 = new Player() { PlayerId = 3, Name = "Bukayo Saka", Age = 21, Position = "Midfield", Nationality = "England" };
            Player p3 = new Player() { PlayerId = 4, Name = "Bruno Fernandes", Age = 28, Position = "Midfield", Nationality = "Portugal" };
            Player p4 = new Player() { PlayerId = 5, Name = "Alisson", Age = 30, Position = "Goalkeeper", Nationality = "Brazil" };
            Player p5 = new Player() { PlayerId = 6, Name = "Gabriel Jesus", Age = 25, Position = "Attacker", Nationality = "Brazil" };


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Championship c1 = new Championship() { ChampionshipId = 2, Name = "LaLiga", Location = "Spain" };

            Team t3 = new Team() { TeamId = 4, Name = "FC Barcelona", Nickname = "Barça" };
            Team t4 = new Team() { TeamId = 5, Name = "Real Madrid", Nickname = "Los Blancos" };
            Team t5 = new Team() { TeamId = 6, Name = "Atlético Madrid", Nickname = "The Reds" };

            Player p6 = new Player() { PlayerId = 7, Name = "Robert Lewandowski", Age = 34, Position = "Attacker", Nationality = "Poland" };
            Player p7 = new Player() { PlayerId = 8, Name = "Karim Benzema", Age = 34, Position = "Attacker", Nationality = "France" };
            Player p8 = new Player() { PlayerId = 9, Name = "Jan Oblak", Age = 29, Position = "Goalkeeper", Nationality = "Slovenia" };
            Player p9 = new Player() { PlayerId = 10, Name = "Sergio Busquets", Age = 34, Position = "Midfield", Nationality = "Spain" };
            Player p10 = new Player() { PlayerId = 11, Name = "Marco Asensio", Age = 26, Position = "Winger", Nationality = "Spain" };
            Player p11 = new Player() { PlayerId = 12, Name = "Koke", Age = 30, Position = "Midfield", Nationality = "Spain" };

            t0.ChampionshipId = c0.ChampionshipId;
            t1.ChampionshipId = c0.ChampionshipId;
            t2.ChampionshipId = c0.ChampionshipId;

            p0.TeamId = t0.TeamId;
            p1.TeamId = t1.TeamId;
            p2.TeamId = t2.TeamId;
            p3.TeamId = t0.TeamId;
            p4.TeamId = t1.TeamId;
            p5.TeamId = t2.TeamId;



            //---------------------------------
            t3.ChampionshipId = c1.ChampionshipId;
            t4.ChampionshipId = c1.ChampionshipId;
            t5.ChampionshipId = c1.ChampionshipId;

            p6.TeamId = t3.TeamId;
            p7.TeamId = t4.TeamId;
            p8.TeamId = t5.TeamId;
            p9.TeamId = t3.TeamId;
            p10.TeamId = t4.TeamId;
            p11.TeamId = t5.TeamId;

            modelBuilder.Entity<Championship>().HasData(c0, c1);
            modelBuilder.Entity<Team>().HasData(t0, t1, t2, t3, t4, t5);
            modelBuilder.Entity<Player>().HasData(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);




            base.OnModelCreating(modelBuilder);
        }
    }
}
