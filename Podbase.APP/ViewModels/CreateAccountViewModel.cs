using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class CreateAccountViewModel : ViewModelBase
    {

        public RelayCommand CreateAccountCommand { get; set; }
        public List<Account> Accounts = Account.Accounts;
        //private Accounts accountsDataAccess = new Accounts();

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand(AddNewAccount);
        }

        private void AddNewAccount()
        {
            Account account = new Account()
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = Password
            };
            if (Account.ValidPassword.IsMatch(account.Password))
            {
                //if (Account.UsernameNotTaken(account.Username))
                //{
                    Accounts.Add(account);
                    NavigationService.Navigate(typeof(LoginPage));
                //}
                //else
                //    LoginViewModel.CreateDialog("taken");

            }
            else
                LoginViewModel.CreateDialog("invalidPassword");
        }

        private string _firstName, _lastName, _username, _password;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

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
    }
}
