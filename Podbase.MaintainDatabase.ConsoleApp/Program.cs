using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.MaintainDatabase.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! In this console app you can maintain your database.");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "donau.hiof.no",
                InitialCatalog = "lhhaugel",
                UserID = "lhhaugel",
                Password = "ze59EYmH"
            };

            var connection = builder.ConnectionString.ToString();
            Console.WriteLine(connection);
            var optionsBuilder = new DbContextOptionsBuilder<PodbaseContext>();
            optionsBuilder.UseSqlServer(connection);

            {
                using (var db = new PodbaseContext(optionsBuilder.Options))
                {
                    //db.Accounts.Add(new Account() { LoginId = 1, FirstName = "Test", LastName = "lol", Username = "testusername", Password = "testpassword" });
                    //var count = db.SaveChanges();
                    //Console.WriteLine("{0} records saved to database", count);

                    Console.WriteLine();
                    Console.WriteLine("All accounts in database:");
                    foreach (var account in db.Accounts)
                    {
                        Console.WriteLine(" - {0}", account.ToString());
                    }
                }
            }
        }
    }
}
