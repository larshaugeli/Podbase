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
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 1, FirstName = "Lars", LastName = "Haugeli", Username = "larshaugeli", Password = "Test123" });
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 2, FirstName = "Sansa", LastName = "Stark", Username = "sansastark", Password = "Test123" });
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 3, FirstName = "Arya", LastName = "Stark", Username = "aryastark", Password = "Test123" });
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 4, FirstName = "Ned", LastName = "Stark", Username = "nedstark", Password = "Test123" });
            modelBuilder.Entity<Account>().HasData(new Account() { UserId = 5, FirstName = "Bran", LastName = "Stark", Username = "branstark", Password = "Test123" });
            Debug.WriteLine("Dummy accounts made.");
        }

        private static void CreateDummyPodcasts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Podcast>().ToTable("Podcasts");
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 1, Name = "Radioresepsjonen", Creator = "NRK", Genre = "Comedy", Description = "Funny podcast", UserId = 1});
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 2, Name = "Misjonen", Creator = "P4", Genre = "Comedy", Description = "Funny podcast", UserId = 1});
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 3, Name = "Misjonen", Creator = "P4", Genre = "Comedy", Description = "Funny podcast", UserId = 1});
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 4, Name = "Serial", Creator = "This American Life", Genre = "True Crime", Description = "Podcast about a murder", UserId = 2});
            modelBuilder.Entity<Podcast>().HasData(new Podcast() { PodcastId = 5, Name = "Serial", Creator = "This American Life", Genre = "True Crime", Description = "Podcast about a murder", UserId = 2});
            Debug.WriteLine("Dummy podcasts made.");
        }

        private static void CreateDummyFriends(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().ToTable("Friends");
            modelBuilder.Entity<Friend>().HasData(new Friend() { ConnectionId = 1, UserId = 1, FriendId = 2 });
            modelBuilder.Entity<Friend>().HasData(new Friend() { ConnectionId = 2, UserId = 1, FriendId = 3 });
            modelBuilder.Entity<Friend>().HasData(new Friend() { ConnectionId = 3, UserId = 1, FriendId = 4 });
            modelBuilder.Entity<Friend>().HasData(new Friend() { ConnectionId = 4, UserId = 2, FriendId = 3 });
            modelBuilder.Entity<Friend>().HasData(new Friend() { ConnectionId = 5, UserId = 2, FriendId = 5 });
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
