using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public Account SelectedAccount;


        public FriendsViewModel()
        {

        }

        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            foreach (Account account in accounts)
            {
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
    }
}
