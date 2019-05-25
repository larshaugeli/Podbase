using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class FriendsViewModel : Observable
    {
        public ObservableCollection<Account> Accounts { get; set; }= new ObservableCollection<Account>();
        public Account SelectedAccount;

        public FriendsViewModel()
        {

        }

        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            var query = from acc in accounts where acc.UserId == LoginViewModel.loggedInUserId select acc;
            foreach (Account account in query)
            {
                Accounts.Add(account);
                Debug.WriteLine("accounts is set");
            }
        }
    }
}
