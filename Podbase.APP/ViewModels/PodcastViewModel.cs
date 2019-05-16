using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class PodcastViewModel : Observable
    {
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public static ObservableCollection<Podcast> Podcasts { get; set; } = new ObservableCollection<Podcast>();
        //private Podcasts podcastsDataAccess = new Podcasts();
        public static List<Podcast> PodcastsList { get; set; }
        public static ListBox PodcastsListBox { get; set; }

        public PodcastViewModel()
        {
            PodcastsList = new List<Podcast>
            {
                new Podcast()
                {
                    Creator = "NRK",
                    Name = "Tore Sagens Podcast",
                    Genre = "Intervju",
                    Description = "Tore Sagen snakker med gjester om temaer."
                },

                new Podcast()
                {
                Creator = "NRK",
                Name = "Radioresepsjonen",
                Genre = "Lettbeint underholdning",
                Description = "To idioter og en bælfeit jævel som snakker om alt og ingenting."
                }
            };
            PodcastsListBox.DataContext = PodcastsList;

            DeleteCommand = new RelayCommand(DeletePodcast);
            AddCommand = new RelayCommand(GoToAddPodcast);
            DeleteCommand = new RelayCommand(DeletePodcast);

        }

        private void GoToAddPodcast()
        {
            NavigationService.Navigate(typeof(PodcastPage));
        }

        private void DeletePodcast()
        {
            foreach (var podcast in PodcastsListBox.SelectedItems.OfType<Podcast>().ToList())
            {
                PodcastsListBox.Items.Remove(podcast);
            }
        }

        //internal async Task LoadPodcastsAsync()
        //{
        //    var podcasts = await podcastsDataAccess.GetPodcastsAsync();
        //    foreach (Podcast podcast in podcasts)
        //        Podcasts.Add(podcast);
        //}
    }
}
