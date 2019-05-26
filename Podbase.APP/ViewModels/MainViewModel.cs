using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<Friend> Friends { get; set; } = FriendsViewModel.Friends;

        public MainViewModel()
        {
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
