using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class PodcastViewModel : ViewModelBase
    {
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public static ObservableCollection<Podcast> Podcasts { get; set; }
        //private Podcasts podcastsDataAccess = new Podcasts();
        //public static List<Podcast> PodcastsList { get; set; }
        //public ListView PodcastListView { get; set; }

        public PodcastViewModel()
        {
            Podcasts = AddPodcastViewModel.AddedPodcasts;

            DeleteCommand = new RelayCommand<Podcast>(podcast => { Podcasts.Remove(podcast); });
            AddCommand = new RelayCommand(GoToAddPodcastPage);
            EditCommand = new RelayCommand<Podcast>(podcast => GoToEditPodcastPage(podcast));
        }

        private void GoToAddPodcastPage()
        {
            NavigationService.Navigate(typeof(AddPodcastPage));
        }

        private void GoToEditPodcastPage(Podcast pod)
        {
            Podcast sel = pod;
            EditPodcastViewModel.SelectedPodcast = sel;
            ShowToastNotification("Alert", sel.Name + " selected.");
            NavigationService.Navigate(typeof(EditPodcastPage));
        }

        private void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(4);
            ToastNotifier.Show(toast);
        }

        //internal async Task LoadPodcastsAsync()
        //{
        //    var podcasts = await podcastsDataAccess.GetPodcastsAsync();
        //    foreach (Podcast podcast in podcasts)
        //        Podcasts.Add(podcast);
        //}
    }
}
