using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    /// <summary>
    /// This view model contains methods relevant to creating an account.
    /// This class creates an account and adds it to the database.
    /// </summary>
    public class CreateAccountViewModel : ViewModelBase
    {
        public ICommand CreateAccountCommand { get; set; }
        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static Accounts AccountDataAccess = new Accounts();
        private string _firstName, _lastName, _username, _password;

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand(AddNewAccount);
        }

        // Gets accounts from database and fills ObservableCollection Accounts in Account model
        internal async Task LoadAccountsAsync()
        {
            var accounts = await AccountDataAccess.GetAccountsAsync();
            foreach (Account account in accounts)
                Accounts.Add(account);
                Debug.WriteLine(Accounts.Count);
        }

        // Creates a new account when "Create Account" is pressed
        public async void AddNewAccount()
        {
            try
            {
                var account = new Account()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Username = Username,
                    Password = Password
                };
                if (UsernameTaken(account.Username))
                    throw new ArgumentException("This username is already taken.");

                if (await AccountDataAccess.AddAccountAsync(account))
                    {
                        Accounts.Add(account);
                    }
                NavigationService.Navigate(typeof(LoginPage));
            }
            catch (ArgumentException exception)
            {
                Misc.CreateDialog("Error", exception.Message);
            }
        }

        public bool UsernameTaken(string username)
        {
            if (Accounts.Count == 0)
                return false;

            foreach (Account account in Accounts)
            {
                if (username.Equals(account.Username))

                    return true;
            }
            return false;
        }

        // Input strings
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

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
