using System;
using System.Data;
using System.Data.SqlClient;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Microsoft.EntityFrameworkCore;
using Podbase.APP.ViewModels;
using Podbase.DataAccess;

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

        public static void CreateDialog(string situation)
        {
            ContentDialog dialog = new ContentDialog();
            {
                switch (situation)
                {
                    case "notExists":
                        dialog.Title = "Error";
                        dialog.Content = "Username and password combination does not exists";
                        break;
                    case "exists":
                        dialog.Title = "Welcome";
                        dialog.Content = "Welcome " + LoginViewModel.LoggedInAccount.Username;
                        break;
                    case "invalidPassword":
                        dialog.Title = "Error";
                        dialog.Content = "Invalid password. Password must include one number, one upper case letter and must be 4 or more characters.";
                        break;
                    case "taken":
                        dialog.Title = "Error";
                        dialog.Content = "Username already taken.";
                        break;
                }
            }
            dialog.CloseButtonText = "OK";
            dialog.ShowAsync();
        }
    }
}
