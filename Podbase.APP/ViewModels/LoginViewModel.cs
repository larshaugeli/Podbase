using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Variables
        public static string loggedInUsername, loggedInFirstName, loggedInLastName;
        public static int loggedInUserId;
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CreateAccount { get; set; }
        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        // Constructor
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser);
            CreateAccount = new RelayCommand(GoToCreateAccount);
        }

        // Methods
        // Logs in user, checks if username and password is in database
        private void LoginUser()
        {
            var account = new Account
            {
                Username = Username,
                Password = Password
            };

            Debug.WriteLine("Account count: " + Accounts.Count);

            bool accountInAccountsList = Accounts.Any(x => x.Username == Username && x.Password == Password);

            if (accountInAccountsList)
            {
                loggedInUsername = account.Username;
                var query = from acc in Accounts where acc.Username == loggedInUsername select acc;

                foreach (Account acc in query)
                {
                    loggedInFirstName = acc.FirstName;
                    loggedInLastName = acc.LastName;
                    loggedInUserId = acc.UserId;
                }   
                Misc.CreateDialog("exists");
                NavigationService.Navigate(typeof(MainPage));

            } else {
                Misc.CreateDialog("notExists");}
        }

        // 
        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            foreach (Account account in accounts)
                Accounts.Add(account);
        }

        // Goes to CreateAccountPage when "Create Account"-button is pressed
        public void GoToCreateAccount()
        {
            NavigationService.Navigate(typeof(CreateAccount));
        }

        // Input strings
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
    }
}
