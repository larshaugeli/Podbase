using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Podbase.Model;

namespace Podbase.DataAccess
{
    public class PodbaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public PodbaseContext(DbContextOptions<PodbaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateDummyAccounts(modelBuilder);
            CreateDummyPodcasts(modelBuilder);
            CreateDummyFriends(modelBuilder);
        }

        private static void CreateDummyAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 1, FirstName = "Lars", LastName = "Haugeli", Username = "larshaugeli", Password = "Sofimjau123" });
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 2, FirstName = "Sofi", LastName = "Mjaupus", Username = "sofimjaupus", Password = "Sofimjau123" });
            Debug.WriteLine("Dummy accounts made.");
        }

        private static void CreateDummyPodcasts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Podcast>().ToTable("Podcasts");
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 1, Name = "Radioresepsjonen", Creator = "NRK", Genre = "Humor", Description = "Gøy", UserId = 1});
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 2, Name = "Misjonen", Creator = "P4", Genre = "Humor", Description = "Hehe", UserId = 1});
            Debug.WriteLine("Dummy podcasts made.");
        }

        private static void CreateDummyFriends(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().ToTable("Friends");
            modelBuilder.Entity<Friend>().HasData(new Friend() { UserId = 1, FriendId = 2 });
            Debug.WriteLine("Dummy friend made.");
        }
    }

    public class PodbaseContextFactory : IDesignTimeDbContextFactory<PodbaseContext>
    {
        public PodbaseContext CreateDbContext(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "donau.hiof.no",
                InitialCatalog = "lhhaugel",
                UserID = "lhhaugel",
                Password = "ze59EYmH"
            };

            var connection = builder.ConnectionString.ToString();

            var optionsBuilder = new DbContextOptionsBuilder<PodbaseContext>().EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(connection, x => x.MigrationsAssembly("Podbase.DataAccess"));

            return new PodbaseContext(optionsBuilder.Options);
        }
    }
}
