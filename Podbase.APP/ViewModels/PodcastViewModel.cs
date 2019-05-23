using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class PodcastViewModel : ViewModelBase
    {
        // Variables
        public ICommand SortCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public static ObservableCollection<Podcast> UsersPodcastsList { get; set; } = new ObservableCollection<Podcast>();
        public static ObservableCollection<Podcast> Podcasts { get; set; } = new ObservableCollection<Podcast>();

        // Constructor
        public PodcastViewModel()
        {
            Podcasts.Clear();
            SortCommand = new RelayCommand(Sort);
            DeleteCommand = new RelayCommand<Podcast>(async podcast => await DeletePodcast(podcast));
            AddCommand = new RelayCommand(GoToAddPodcastPage);
            EditCommand = new RelayCommand<Podcast>(podcast => GoToEditPodcastPage(podcast));
        }

        // Methods
        internal async Task LoadPodcastsAsync()
        {
            var podcasts = await AddPodcastViewModel.podcastsDataAccess.GetPodcastsAsync();
            var query = from pod in podcasts where pod.UserId == LoginViewModel.loggedInUserId select pod;
            foreach (Podcast podcast in query)
                Podcasts.Add(podcast);
        }

        // Goes to Edit Podcast page when pressed 
        private async Task DeletePodcast(Podcast pod)
        {
            EditPodcastViewModel.SelectedPodcast = pod;
            if (pod == null)
            {
                Misc.ShowToastNotification("Error", "No podcast selected.", 1);
            }
            else
            {
                if (await AddPodcastViewModel.podcastsDataAccess.DeletePodcastAsync(pod))
                {
                    Podcasts.Remove(pod);
                }
                Misc.ShowToastNotification("Alert", pod.Name + " deleted.", 1);
            }
        }

        // Goes to Add podcast page when pressed "+"-button
        private void GoToAddPodcastPage()
        {
            NavigationService.Navigate(typeof(AddPodcastPage));
        }

        // Sorts podcasts list
        //TODO: if sorted asc, sort desc
        private void Sort()
        {
            SortPodcasts(Podcasts, podcast => podcast.Name);
        }

        // Helping method to sorting
        public void SortPodcasts<TSource, TKey>(ObservableCollection<TSource> observableCollection, Func<TSource, TKey> keySelector)
        {
            var a = observableCollection.OrderBy(keySelector).ToList();
            observableCollection.Clear();
            foreach (var b in a)
            {
                observableCollection.Add(b);
            }
        }

        // Goes to Edit Podcast page when pressed 
        private void GoToEditPodcastPage(Podcast pod)
        {
            EditPodcastViewModel.SelectedPodcast = pod;
            if (pod == null)
            {
                Misc.ShowToastNotification("Error", "No podcast selected.", 1);
            }
            else
            {
                Misc.ShowToastNotification("Alert", pod.Name + " selected.", 1);
                NavigationService.Navigate(typeof(EditPodcastPage));
            }
        }
    }
}
