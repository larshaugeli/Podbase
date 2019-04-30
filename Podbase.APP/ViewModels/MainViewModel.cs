using System;
using System.Collections.Generic;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class MainViewModel : Observable
    {
        public List<Podcast> PopularPodcasts { get; set; }

        public MainViewModel()
        {
            PopularPodcasts = new List<Podcast>
            {
                new Podcast()
                {
                    Creator = "This American Life",
                    Name = "Serial",
                    Genre = "True crime",
                    Description = "True crime about a murder."
                },

                new Podcast()
                {
                    Creator = "Reply All",
                    Name = "ABC",
                    Genre = "Entertainment",
                    Description = "Podcast about the internet."
                }
            };
        }

        private string _username = LoginViewModel.loggedInUsername;

        public string Username
        {
            get { return "Welcome " + _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
    }
}
