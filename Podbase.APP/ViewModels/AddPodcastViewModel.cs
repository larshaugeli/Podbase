﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    /// <summary>
    /// This view model contains code relevant to adding a podcast.
    /// This class adds a podcast to the list in Podcast page.
    /// </summary>
    public class AddPodcastViewModel : ViewModelBase
    {
        public static ObservableCollection<Podcast> AddedPodcasts = new ObservableCollection<Podcast>();
        public static readonly Podcasts PodcastsDataAccess = new Podcasts();
        public ICommand CreatePodcastCommand { get; set; }

        public AddPodcastViewModel()
        {
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
