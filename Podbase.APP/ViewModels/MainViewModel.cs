using System;

using Podbase.APP.Helpers;

namespace Podbase.APP.ViewModels
{
    public class MainViewModel : Observable
    {
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
