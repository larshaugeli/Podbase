using System.Collections.ObjectModel;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AddPodcastViewModel : ViewModelBase
    {
        public static ObservableCollection<Podcast> AddedPodcasts = new ObservableCollection<Podcast>();
        public static readonly Podcasts PodcastsDataAccess = new Podcasts();
        public ICommand CreatePodcastCommand { get; set; }

        public AddPodcastViewModel()
        {
            //CreatePodcastCommand = new RelayCommand(AddNewPodcast);
            CreatePodcastCommand = new RelayCommand<string>(async input =>
                                                            {
                                                                var pod = new Podcast() {
                                                                    Name = Name, Creator = Creator, Genre = Genre, Description = Description, UserId = LoginViewModel.LoggedInAccount.UserId
                                                                };
                                                                if (await PodcastsDataAccess.AddPodcastAsync(pod))
                                                                    AddedPodcasts.Add(pod);
                                                                NavigationService.Navigate(typeof(PodcastPage));
                                                            }, input => !string.IsNullOrEmpty(input));
        }

        //Add new podcast, executes when pressed "Add podcast"-button
        //public async void AddNewPodcast()
        //{
        //    var podcast = new Podcast()
        //    {
        //        Name = Name,
        //        Creator = Creator,
        //        Genre = Genre,
        //        Description = Description,
        //        UserId = LoginViewModel.LoggedInAccount.UserId
        //    };
        //    if (await PodcastsDataAccess.AddPodcastAsync(podcast))
        //    {
        //        AddedPodcasts.Add(podcast);
        //    }
        //    NavigationService.Navigate(typeof(PodcastPage));
        //}

        // Input strings
        private string _name, _creator, _genre, _description;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("_name");
            }
        }

        public string Creator
        {
            get => _creator;
            set
            {
                _creator = value;
                OnPropertyChanged("_creator");  
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged("_genre");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("_description");
            }
        }
    }
}
