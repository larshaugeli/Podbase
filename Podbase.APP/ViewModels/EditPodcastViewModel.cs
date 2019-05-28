﻿using System;
using System.Linq;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class EditPodcastViewModel : ViewModelBase
    {
        public RelayCommand EditPodcastCommand { get; set; }
        public static Podcast SelectedPodcast;
        public int SelectedPodcastId = SelectedPodcast.PodcastId;

        public EditPodcastViewModel()
        {
            EditPodcastCommand = new RelayCommand(EditPodcast);
        }

        // Edits selected podcast. Modifies it in database
        public void EditPodcast()
        {
            SelectedPodcast = new Podcast()
            {
                Name = Name,
                Creator = Creator,
                Genre = Genre,
                Description = Description,
                UserId = LoginViewModel.LoggedInAccount.UserId,
                PodcastId = SelectedPodcastId
            };

            // Modifies table row in database
            using (var db = new PodbaseContext(Misc.OptionsBuilder().Options))
            {
                var result = db.Podcasts.SingleOrDefault(b => b.PodcastId == SelectedPodcastId);

                if (result != null)
                {
                    result.Name = Name;
                    result.Creator = Creator;
                    result.Genre = Genre;
                    result.Description = Description;
                    db.SaveChanges();
                }
            };
            NavigationService.Navigate(typeof(PodcastPage));
        }

        // Input strings
        private String _name, _creator, _genre, _description;

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
