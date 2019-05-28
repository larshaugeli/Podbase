using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class PodcastViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public static ObservableCollection<Podcast> Podcasts { get; set; } = new ObservableCollection<Podcast>();

        public PodcastViewModel()
        {
            Podcasts.Clear();
            AddCommand = new RelayCommand(GoToAddPodcastPage);
            DeleteCommand = new RelayCommand<Podcast>(DeletePodcast);
            EditCommand = new RelayCommand<Podcast>(GoToEditPodcastPage);
            SortCommand = new RelayCommand(SortPodcasts);
        }

        // Gets podcasts from database and fills ObservableCollection Podcasts
        internal async Task LoadPodcastsAsync()
        {
            var podcasts = await AddPodcastViewModel.PodcastsDataAccess.GetPodcastsAsync();
            var query = from pod in podcasts where pod.UserId == LoginViewModel.LoggedInAccount.UserId select pod;
            foreach (Podcast podcast in query)
                Podcasts.Add(podcast);
        }

        // Goes to AddPodcastPage when pressed "+"-button
        private static void GoToAddPodcastPage()
        {
            NavigationService.Navigate(typeof(AddPodcastPage));
        }

        // Deletes selected podcast from ObservableCollection and database
        private static async void DeletePodcast(Podcast pod)
        {
            EditPodcastViewModel.SelectedPodcast = pod;
            if (pod == null)
                Misc.ShowToastNotification("Error", "No podcast selected.", 1);
            else
                if (await AddPodcastViewModel.PodcastsDataAccess.DeletePodcastAsync(pod))
                {
                    Podcasts.Remove(pod);
                    Misc.ShowToastNotification("Alert", pod.Name + " deleted.", 1);
                }
        }

        // Goes to EditPodcastPage when pressed "Edit"-symbol 
        private static void GoToEditPodcastPage(Podcast pod)
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


        // Sorts podcasts list when pressed "Sort"-button
        private void SortPodcasts()
        {
            Sort(Podcasts, podcast => podcast.Name);
        }

        // Sorts podcasts list
        public void Sort<TSource, TKey>(ObservableCollection<TSource> observableCollection, Func<TSource, TKey> keySelector)
        {
            var a = observableCollection.OrderBy(keySelector).ToList();
            observableCollection.Clear();
            foreach (var b in a)
            {
                observableCollection.Add(b);
            }
        }
    }
}
