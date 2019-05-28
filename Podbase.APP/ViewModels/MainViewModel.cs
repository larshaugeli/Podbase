using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class MainViewModel : Observable
    {
        public MainViewModel()
        {
        }

        private string _username = LoginViewModel.LoggedInAccount.Username;

        public string Username
        {
            get => "Welcome " + _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
    }
}
