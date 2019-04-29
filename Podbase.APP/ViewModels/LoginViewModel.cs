using System;

using Podbase.APP.Helpers;

namespace Podbase.APP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login());
        }

        private Action Login()
        {
           
        }


        private String _username, _password;

        public string username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("username");
            }
        }

        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("password");
            }
        }
    }
}
