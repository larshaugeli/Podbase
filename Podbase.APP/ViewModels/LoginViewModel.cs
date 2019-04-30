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
            var accounts = Account.Accounts;
            var account = new Account
            {
                Username = Username,
                Password = Password
            };

            bool accountInAccountsList = accounts.Any(x => x.Username == Username && x.Password == Password);
            if (accountInAccountsList)
            {
                loggedInUsername = account.Username;
                loggedInFirstName = account.FirstName;
                loggedInLastName = account.LastName;
                CreateDialog("exists");
                NavigationService.Navigate(typeof(MainPage));

            } else {
                CreateDialog("notExists");}
        }

        private String _username, _password;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public static void CreateDialog(string situation)
        {
            ContentDialog dialog = new ContentDialog();
            {
                switch (situation)
                {
                    case "notExists":
                        dialog.Title = "Error";
                        dialog.Content = "Username and password combination does not exists";
                        break;
                    case "exists":
                        dialog.Title = "Welcome";
                        dialog.Content = "Welcome " + loggedInUsername;
                        break;
                    case "invalidPassword":
                        dialog.Title = "Error";
                        dialog.Content = "Invalid password. Password must include one number, one upper case letter and must be 4 or more characters.";
                        break;
                    case "taken":
                        dialog.Title = "Error";
                        dialog.Content = "Username already taken.";
                        break;
                }
            }
            dialog.CloseButtonText = "OK";
            dialog.ShowAsync();
        }
    }
}
