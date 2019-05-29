using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class CreateAccountViewModel : ViewModelBase
    {
        public ICommand CreateAccountCommand { get; set; }
        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static Accounts AccountDataAccess = new Accounts();
        private string _firstName, _lastName, _username, _password;

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand<string>(async input =>
                                                            {
                                                                var account = new Account() { FirstName = FirstName, LastName = LastName, Username = Username, Password = Password };
                                                                Debug.WriteLine(account.Username + " " + account.FirstName + " " + account.LastName + " " + account.Password);
                                                                {
                                                                    if (await AccountDataAccess.AddAccountAsync(account))
                                                                        Debug.WriteLine("added");
                                                                        Accounts.Add(account);
                                                                    NavigationService.Navigate(typeof(LoginPage));
                                                                }
                                                            });
        }

        //// Creates a new account when "Create Account" is pressed
        //public async void AddNewAccount()
        //{
        //    var account = new Account()
        //    {
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        Username = Username,
        //        Password = Password
        //    };

        //    // Adds to database and to ObservableCollection
        //    if (Account.ValidPassword.IsMatch(account.Password))
        //    {
        //        //if (Account.UsernameNotTaken(account.Username))
        //        //{
        //        if (await AccountDataAccess.AddAccountAsync(account))
        //        {
        //            Accounts.Add(account);
        //        }
        //        NavigationService.Navigate(typeof(LoginPage));
        //        //}
        //        //else
        //        //    LoginViewModel.CreateDialog("taken");

        //    }
        //    else
        //        Misc.CreateDialog("invalidPassword");
        //}

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
