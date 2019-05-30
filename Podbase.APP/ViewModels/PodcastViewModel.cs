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
            EditCommand = new RelayCommand<Podcast>(GoToEditPodcastPage, podcast => podcast != null);
            SortCommand = new RelayCommand<Podcast>(pod => Sort(Podcasts, podcast => podcast.Name));

            DeleteCommand = new RelayCommand<Podcast>(async podcast =>
                                                    {
                                                        if (await AddPodcastViewModel.PodcastsDataAccess.DeletePodcastAsync(podcast))
                                                            Podcasts.Remove(podcast);
                                                    }, podcast => podcast != null);
        }

        // Gets podcasts from database and fills ObservableCollection Podcasts
        internal async Task LoadPodcastsAsync()
        {
            var podcasts = await AddPodcastViewModel.PodcastsDataAccess.GetPodcastsAsync();
            var query = from pod in podcasts where pod.UserId == LoginViewModel.LoggedInAccount.UserId select pod;
            foreach (Podcast podcast in query)
                Podcasts.Add(podcast);
        }

        // Navigates to EditPodcastPage when pressed "Edit"-symbol 
        private static void GoToEditPodcastPage(Podcast pod)
        {
            EditPodcastViewModel.SelectedPodcast = pod;
            Misc.ShowToastNotification("Alert", pod.Name + " selected.", 1);
            NavigationService.Navigate(typeof(EditPodcastPage));
        }

        // Navigates to AddPodcastPage when pressed "+"-button
        private static void GoToAddPodcastPage() { NavigationService.Navigate(typeof(AddPodcastPage)); }

        // Sorts podcasts list
        public static void Sort<TSource, TKey>(ObservableCollection<TSource> observableCollection, Func<TSource, TKey> keySelector)
        {
            var a = observableCollection.OrderBy(keySelector).ToList();
            observableCollection.Clear();
            foreach (var b in a)
            {
                observableCollection.Add(b);
            }
        }

        // Navigates to specific podcast
        public void GoToSelectedPodcast(Podcast podcast)
        {
            ChosenPodcastViewModel.SelectedPodcast = podcast;
            NavigationService.Navigate(typeof(ChosenPodcastPage));
        }
    }
}
