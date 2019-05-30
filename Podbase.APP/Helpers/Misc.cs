using System;
using System.Data;
using System.Data.SqlClient;
using Windows.Foundation;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Microsoft.EntityFrameworkCore;
using Podbase.APP.ViewModels;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.APP.Helpers
{
    public class Misc
    {
        public static DbContextOptionsBuilder<PodbaseContext> OptionsBuilder()
        {
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

            return optionsBuilder;
        }

        public static void TruncateTables()
        {
            using (var db = new PodbaseContext(OptionsBuilder().Options))
            {
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
            }
        }

        // Toast
        public static void ShowToastNotification(string title, string stringContent, int showLength)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml) { ExpirationTime = DateTime.Now.AddSeconds(showLength) };
            ToastNotifier.Show(toast);
        }

        public static IAsyncOperation<IUICommand> CreateMessageDialog(string title, string message)
        {
            MessageDialog messageDialog = new MessageDialog(message, title);
            return messageDialog.ShowAsync();
        }

        public static void CreateDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog();
            {
                dialog.Title = title;
                dialog.Content = message;
            }
            dialog.CloseButtonText = "OK";
            dialog.ShowAsync();
        }

        public static void CreateDummyAccounts()
        {
            TruncateTables();
            using (var db = new PodbaseContext(OptionsBuilder().Options))
            {
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
            }  
        }
    }
}
