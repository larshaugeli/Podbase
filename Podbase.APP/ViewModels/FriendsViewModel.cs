using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Podbase.APP.DataAccess;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static ObservableCollection<Account> FriendsAccounts { get; set; } = new ObservableCollection<Account>();
        public static Account SelectedAccount;
        public static Friends FriendsDataAccess = new Friends();

        public FriendsViewModel()
        {
            FriendsAccounts.Clear();
        }

        // Gets Accounts and Friends from database and adds to ObservableCollection
        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.AccountDataAccess.GetAccountsAsync();

            foreach (var account in accounts)
            {
                if (account.UserId != LoginViewModel.loggedInUserId)
                    Accounts.Add(account);
            }

            var friends = await FriendsDataAccess.GetFriendsAsync();
            var friendsAccountsQuery = from friend in friends where friend.UserId == LoginViewModel.loggedInUserId select friend.FriendId;
            
            foreach (int friendId in friendsAccountsQuery)
            {
                foreach (Account account in accounts)
                    if (account.UserId == friendId)
                        FriendsAccounts.Add(account);
            }

            var friendsQuery = from queryFriend in friends where queryFriend.UserId == LoginViewModel.loggedInUserId select queryFriend;
            foreach (Friend dbFriend in friendsQuery)
                AccountViewModel.Friends.Add(dbFriend);
        }

        // Navigates to selected account when selected account is double clicked
        public void GoToSelectedAccount(Account account)
        {
            SelectedAccount = account;
            AccountViewModel.FromFriendsPage = true;
            NavigationService.Navigate(typeof(AccountPage));
        }
    }
}
