using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;
using Remotion.Linq.Clauses;

namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public RelayCommand SaveTextCommand { get; set; }
        public Account LoggedInAccount;

        public AccountViewModel()
        {
            SaveTextCommand = new RelayCommand(SaveText);
        }

        private async void SaveText()
        {
            Account[] accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            var query = from acc in LoginViewModel.Accounts where acc.Username == LoginViewModel.loggedInUsername select acc;
            foreach (Account acc in accounts)
            {
                foreach (Account account in query)
                {
                    LoggedInAccount = account;
                }
            }
            LoggedInAccount.AboutMe = AboutMe;
            Debug.WriteLine("Account: " + LoggedInAccount.Username + " " + LoggedInAccount.UserId + " " + LoggedInAccount.AboutMe);
            string json = LoggedInAccount.ConvertFromObjectToJson();
            
            Account accountJson = new Account(json);
            Debug.WriteLine("jsondrit: " + accountJson.Username + " " + accountJson.UserId + " " + accountJson.AboutMe);

            if (await CreateAccountViewModel.accountDataAccess.DeleteAccountAsync(LoggedInAccount))
            {
                Debug.WriteLine("Deleted " + LoggedInAccount.Username + " " + LoggedInAccount.AboutMe);
            }
            

            LoggedInAccount = new Account()
            {
                UserId = LoggedInAccount.UserId,
                Username = LoggedInAccount.Username,
                Password = LoggedInAccount.Password,
                FirstName = LoggedInAccount.FirstName,
                LastName = LoggedInAccount.LastName,
                AboutMe = LoggedInAccount.AboutMe
            };
            Debug.WriteLine(LoggedInAccount.Username + " " + LoggedInAccount.UserId + " " + LoggedInAccount.AboutMe);

            if (await CreateAccountViewModel.accountDataAccess.AddAccountAsync(LoggedInAccount))
            {
                Debug.WriteLine("yes");
                CreateAccountViewModel.Accounts.Add(LoggedInAccount);
            }
            
        }

        private string _username = LoginViewModel.loggedInUsername;
        private string _firstName = LoginViewModel.loggedInFirstName;
        private string _lastName = LoginViewModel.loggedInLastName;
        private string _aboutMe;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

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

        public string AboutMe
        {
            get { return _aboutMe; }
            set
            {
                _aboutMe = value;
                OnPropertyChanged("AboutMe");
            }
        }
    }
}
