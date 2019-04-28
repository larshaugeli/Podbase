using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AddPodcastViewModel : Observable
    {
        //public ObservableCollection<Podcast> Podcasts { get; set; } = new ObservableCollection<Podcast>();

        public void AddNewPodcast()
        {
            Podcast podcast = new Podcast()
            {
                Name = name,
                Creator = creator,
                Genre = genre,
                Description = description
            };
            PodcastViewModel.PodcastsList.Add(podcast);
        }

        private String _name, _creator, _genre, _description;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        public string creator
        {
            get { return _creator; }
            set
            {
                _creator = value;
                OnPropertyChanged("creator");
            }
        }

        public string genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged("genre");
            }
        }

        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("description");
            }
        }
    }
}
