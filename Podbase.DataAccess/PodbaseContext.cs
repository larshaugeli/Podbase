﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Podbase.Model;

namespace Podbase.DataAccess
{
    public class PodbaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public PodbaseContext(DbContextOptions<PodbaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateDummyAccounts(modelBuilder);
        }

        private static void CreateDummyAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account() { LoginId = 1, FirstName = "Lars", LastName = "Haugeli", Username = "larshaugeli", Password = "Sofimjau123"});
            modelBuilder.Entity<Account>().HasData(new Account() { LoginId = 2, FirstName = "Sofi", LastName = "Mjaupus", Username = "sofimjaupus", Password = "Sofimjau123"});
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "donau.hiof.no",
                InitialCatalog = "lhhaugel",
                UserID = "lhhaugel",
                Password = "ze59EYmH"
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString.ToString());
        }
    }

    public class EntertainmentContextFactory : IDesignTimeDbContextFactory<PodbaseContext>
    {
        public PodbaseContext CreateDbContext(string[] args)
        {
            var connection = "Insert correct string here";

            var optionsBuilder = new DbContextOptionsBuilder<PodbaseContext>();
            optionsBuilder.UseSqlServer(connection, x => x.MigrationsAssembly("Podbase.MaintainDatabase.ConsoleApp"));

            return new PodbaseContext(optionsBuilder.Options);
        }
    }
}
