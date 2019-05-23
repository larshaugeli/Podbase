using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    class EditPodcastViewModel : ViewModelBase
    {
        public RelayCommand EditPodcastCommand { get; set; }
        public static Podcast SelectedPodcast;
        public int SelectedPodcastId = SelectedPodcast.PodcastId;

        public EditPodcastViewModel()
        {
            EditPodcastCommand = new RelayCommand(EditPodcast);
        }

        // Edit podcasts. Deletes podcast and adds a new one. //TODO change this method so it updates the element, so PodcastId stays the same
        public async void EditPodcast()
        {
            await AddPodcastViewModel.podcastsDataAccess.DeletePodcastAsync(SelectedPodcast);
            SelectedPodcast = new Podcast()
            {
                Name = Name,
                Creator = Creator,
                Genre = Genre,
                Description = Description,
                UserId = LoginViewModel.loggedInUserId,
                PodcastId = SelectedPodcastId
            };
            if (await AddPodcastViewModel.podcastsDataAccess.AddPodcastAsync(SelectedPodcast))
            {
                AddPodcastViewModel.AddedPodcasts.Add(SelectedPodcast);
            }
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
    }
}
