using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public static string loggedInUsername, loggedInFirstName, loggedInLastName;
        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser);
        }

        private void LoginUser()
        {
            var accounts = CreateAccountViewModel.Accounts;
            var account = new Account
            {
                Username = username,
                Password = password
            };
            Debug.WriteLine("Username: " + account.Username + ", Password: " + account.Password);
            bool accountInAccountsList = accounts.Any(x => x.Username == username && x.Password == password);
            if (accountInAccountsList)
            {
                Debug.WriteLine("Account exists");
                createDialog(true);
                loggedInUsername = account.Username;
                loggedInFirstName = account.FirstName;
                loggedInLastName = account.LastName;
                NavigationService.Navigate(typeof(MainPage));

            } else { createDialog(false);}
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

        private void createDialog(bool success)
        {
            if (success == true)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Welcome",
                    Content = "Welcome " + username,
                    CloseButtonText = "OK"
                };
                dialog.ShowAsync();
            }
            else if (success == false)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Username and password combination does not exists",
                    CloseButtonText = "OK"
                };
                dialog.ShowAsync();
            }
        }
    }
}
