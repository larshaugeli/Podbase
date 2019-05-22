using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.EntityFrameworkCore;
using Podbase.DataAccess;

namespace Podbase.APP.Helpers
{
    public class Misc
    {
        public static void TruncateTables()
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

            using (var db = new PodbaseContext(optionsBuilder.Options))
            {
                try
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Accounts]");
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Podcasts]");
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
    }
}
