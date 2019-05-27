﻿using System.Collections.ObjectModel;
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
        public static Podcasts PodcastsDataAccess = new Podcasts();
        public RelayCommand CreatePodcastCommand { get; set; }

        public AddPodcastViewModel()
        {
            CreatePodcastCommand = new RelayCommand(AddNewPodcast);
        }

        // Add new podcast, executes when pressed "Add podcast"-button
        public async void AddNewPodcast()
        {
            Podcast podcast = new Podcast()
            {
                Name = Name,
                Creator = Creator,
                Genre = Genre,
                Description = Description,
                UserId = LoginViewModel.loggedInUserId
            };
            if (await PodcastsDataAccess.AddPodcastAsync(podcast))
            {
                AddedPodcasts.Add(podcast);
            }
            NavigationService.Navigate(typeof(PodcastPage));
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
