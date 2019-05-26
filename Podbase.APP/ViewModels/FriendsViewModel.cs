using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        public static Account SelectedAccount;

        public FriendsViewModel()
        {

        }

        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            foreach (Account account in accounts)
            {
                if (account.UserId != LoginViewModel.loggedInUserId)
                Accounts.Add(account);
                Debug.WriteLine("accounts is set");
                Debug.WriteLine("Accounts freinds count: " +  Accounts.Count);
                WriteAccountsInDebug();
            }
        }

        public void WriteAccountsInDebug()
        {
            foreach (var account in Accounts)
            {
                Debug.WriteLine(account.Username + " " + account.UserId);
            }
        }

        public void GoToSelectedAccount(Account account)
        {
            SelectedAccount = account;
            Debug.WriteLine("friendsviewodel selcetedAccount: " + SelectedAccount.Username);
            AccountViewModel.FromFriendsPage = true;
            NavigationService.Navigate(typeof(AccountPage));
        }
    }
}
