using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
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
            var optionsBuilder = new DbContextOptionsBuilder<PodbaseContext>();
            optionsBuilder.UseSqlServer(connection);

            {
                using (var db = new PodbaseContext(optionsBuilder.Options))
                {
                    // truncates tables to avoid duplicates
                    try
                    {
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Accounts]");
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Podcasts]");
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Friends]");
                    }
                    catch (DataException e)
                    {
                        Console.WriteLine("Exception of type {0} occurred.",
                            e.GetType());
                    }

                    db.Accounts.Add(new Account() { FirstName = "Lars", LastName = "Haugeli", Username = "larshaugeli", Password = "Test123" });
                    db.Accounts.Add(new Account() { FirstName = "Sansa", LastName = "Stark", Username = "sansastark", Password = "Test123" });
                    db.Accounts.Add(new Account() { FirstName = "Arya", LastName = "Stark", Username = "aryastark", Password = "Test123" });
                    db.Accounts.Add(new Account() { FirstName = "Ned", LastName = "Stark", Username = "nedstark", Password = "Test123" });
                    db.Accounts.Add(new Account() { FirstName = "Bran", LastName = "Stark", Username = "branstark", Password = "Test123" });

                    db.Podcasts.Add(new Podcast() { Name = "Radioresepsjonen", Creator = "NRK", Genre = "Comedy", Description = "Funny podcast", UserId = 1 });
                    db.Podcasts.Add(new Podcast() { Name = "Misjonen", Creator = "P4", Genre = "Comedy", Description = "Funny podcast", UserId = 1 });
                    db.Podcasts.Add(new Podcast() { Name = "Serial", Creator = "This American Life", Genre = "True Crime", Description = "Podcast about a murder", UserId = 2 });

                    db.Friends.Add(new Friend() { UserId = 1, FriendId = 2 });
                    db.Friends.Add(new Friend() { UserId = 2, FriendId = 3 });
                    db.Friends.Add(new Friend() { UserId = 2, FriendId = 4 });
                    db.Friends.Add(new Friend() { UserId = 3, FriendId = 1 });
                    db.SaveChanges();

                    var count = db.SaveChanges();
                    Console.WriteLine("{0} records saved to database", count);

                    Console.WriteLine();
                    Console.WriteLine("All accounts in database:");
                    foreach (var account in db.Accounts)
                    {
                        Console.WriteLine(" - {0}", account.Username.ToString());
                    }

                    Console.WriteLine();
                    Console.WriteLine("All podcasts in database:");
                    foreach (var podcast in db.Podcasts)
                    {
                        Console.WriteLine(" - {0}", podcast.Name.ToString());
                    }

                    Console.WriteLine();
                    Console.WriteLine("All friends in database:");
                    foreach (var friend in db.Friends)
                    {
                        Console.WriteLine(" - {0}", "UserId " + friend.UserId + " is friends with UserId " + friend.FriendId);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
