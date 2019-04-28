using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class PodcastViewModel : Observable
    {
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<Podcast> Podcasts { get; set; } = new ObservableCollection<Podcast>();
        private Podcasts podcastsDataAccess = new Podcasts();

        public PodcastViewModel()
        {
            //AddCommand = new RelayCommand<string>(async param =>
            //{
            //    var podcast = new Podcast() { PodcastId = 1, Creator = "Tore Sagen", Name = "Tore Sagens Podcast", Genre = "Intervju", Description = "Intervju med forskjellige folk.", Rating = 5 };

            //    if (await podcastsDataAccess.AddPodcastAsync(podcast))
            //        Podcasts.Add(podcast);
            //}, param => !string.IsNullOrEmpty(param));

            //DeleteCommand = new RelayCommand<Podcast>(async param =>
            //{
            //    if (await podcastsDataAccess.DeletePodcastAsync((Podcast)param))
            //        Podcasts.Remove(param);
            //}, param => param != null);
    }

        internal async Task LoadPodcastsAsync()
        {
            var podcasts = await podcastsDataAccess.GetPodcastsAsync();
            foreach (Podcast podcast in podcasts)
                Podcasts.Add(podcast);
        }
    }
}
