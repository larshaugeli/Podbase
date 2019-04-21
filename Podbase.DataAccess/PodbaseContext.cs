using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Podbase.DataAccess
{
    class PodbaseContext
    {
        public DbSet<Account> Accounts { get; set; }

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
}
