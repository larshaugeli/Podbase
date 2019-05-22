using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AddPodcastViewModel : ViewModelBase
    {
        // Variables
        public static ObservableCollection<Podcast> AddedPodcasts = new ObservableCollection<Podcast>();
        public static Podcasts podcastsDataAccess = new Podcasts();
        public RelayCommand CreatePodcastCommand { get; set; }

        // Constructor
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
                Description = Description
            };
            if (await podcastsDataAccess.AddPodcastAsync(podcast))
            {
                AddedPodcasts.Add(podcast);
            }

            WritePodcastList();
            NavigationService.Navigate(typeof(PodcastPage));
        }

       
        // Input strings
        private String _name, _creator, _genre, _description;

        public string Name
        {
            get { return _name; }
            set
            {
                this._name = value;
                OnPropertyChanged("_name");
            }
        }

        public string Creator
        {
            get { return _creator; }
            set
            {
                {
                    this._creator = value;
                    OnPropertyChanged("_creator");
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                {
                    this._genre = value;
                    OnPropertyChanged("_genre");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                {
                    this._description = value;
                    OnPropertyChanged("_description");
                }
            }
        }

        // Debug
        private void WritePodcastList()
        {
            foreach (var pod in AddedPodcasts)
            {
                Debug.WriteLine(pod.Name + " - " + pod.Creator + " - " + pod.Description + " - " + pod.Genre);
            }
        }
    }
}
