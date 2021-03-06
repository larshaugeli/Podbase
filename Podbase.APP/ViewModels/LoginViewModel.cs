﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    /// <summary>
    /// This view model contain methods relevant to logging in to the application.
    /// This class logs a user into the application. 
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccount { get; set; }
        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static Account LoggedInAccount;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser);
            CreateAccount = new RelayCommand(GoToCreateAccount);
        }

        // Gets accounts from database and fills ObservableCollection Accounts
        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.AccountDataAccess.GetAccountsAsync();
            foreach (Account account in accounts)
                Accounts.Add(account);
        }

        // Logs in user if an account with typed in username and password is in database
        private void LoginUser()
        {
            try
            {
                var account = new Account
                {
                    Username = Username,
                    Password = Password
                };

                bool accountInAccounts = Accounts.Any(x => x.Username == Username && x.Password == Password);

                if (accountInAccounts)
                {
                    var query = from acc in Accounts where acc.Username == account.Username select acc;

                    foreach (Account acc in query)
                    {
                        LoggedInAccount = new Account()
                        {
                            Username = account.Username,
                            Password = account.Password,
                            FirstName = acc.FirstName,
                            LastName = acc.LastName,
                            UserId = acc.UserId
                        };
                    }

                    Misc.CreateDialog("Welcome", "Welcome " + LoggedInAccount.Username);
                    NavigationService.Navigate<ShellPage>();
                    NavigationService.Navigate(typeof(MainPage));
                }
                else
                {
                    Misc.CreateDialog("Error", "Username and password combination does not exists");
                }
            }
            catch (ArgumentException exception)
            {
                Misc.CreateDialog("Error", exception.Message);
            }
        }

        // Navigates to CreateAccountPage when "Create Account"-button is pressed
        public void GoToCreateAccount() { NavigationService.Navigate(typeof(CreateAccount)); }

        // Input strings
        private string _username, _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

    }
}
