using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AddPodcastViewModel : ViewModelBase
    {
        public RelayCommand CreatePodcastCommand { get; set; }

        public AddPodcastViewModel()
        {
            CreatePodcastCommand = new RelayCommand(AddNewPodcast);
        }

        //public AddPodcastViewModel()
        //{
        //    Podcasts.Add(new Podcast()
        //    {
        //        Name = "Serial",
        //        Creator = "This American Life",
        //        Genre = "True Crime",
        //        Description = "True crime podcast about a murder in USA."
        //    });
        //    CreatePodcastCommand = new RelayCommand(AddNewPodcast);
        //}

        public void AddNewPodcast()
        {
            Podcast podcast = new Podcast()
            {
                Name = Name,
                Creator = Creator,
                Genre = Genre,
                Description = Description
            };
            PodcastViewModel.Podcasts.Add(podcast);
            PodcastViewModel.PodcastsList.Add(podcast);

        }

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
