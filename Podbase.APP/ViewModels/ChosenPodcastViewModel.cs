﻿using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    /// <summary>
    /// This view model contains code relevant to the ChosenPodcast page.
    /// This class displays info about a podcast.
    /// </summary>
    public class ChosenPodcastViewModel : ViewModelBase
    {
        public static Podcast SelectedPodcast;
        private string _name, _creator, _genre, _description;

        public ChosenPodcastViewModel()
        {
            _name = SelectedPodcast.Name;
            _creator = SelectedPodcast.Creator;
            _genre = SelectedPodcast.Genre;
            _description = SelectedPodcast.Description;
        }

        // Input strings
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Creator
        {
            get => _creator;
            set
            {
                _creator = value;
                OnPropertyChanged("Creator");
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
    }
}
