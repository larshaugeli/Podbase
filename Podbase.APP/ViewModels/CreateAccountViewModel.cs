using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.Logging;
using Podbase.APP.Annotations;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class CreateAccountViewModel : ViewModelBase
    {
        public RelayCommand CreateAccountCommand { get; set; }
        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        private Accounts accountsDataAccess = new Accounts();

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new RelayCommand(AddNewAccount);
        }

        private void AddNewAccount()
        {
            Account account = new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };
            Accounts.Add(account);
            Debug.WriteLine(Accounts.Count);
            Debug.WriteLine("FirstName: "+ account.FirstName);
            Debug.WriteLine("LastName: " + account.LastName);
            Debug.WriteLine("Username: " + account.Username);
            Debug.WriteLine("Password: " + account.Password);
            Debug.WriteLine(Accounts.ToString());
        }
        
        private String _firstName, _lastName, _username, _password;

        public string firstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("firstName");
            }
        }

        public string lastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("lastName");
            }
        }

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
