using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        public ICommand DeleteCommand { get; set; }
        public ICommand SortAllUsersCommand { get; set; }
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public static ObservableCollection<Account> FriendsAccounts { get; set; } = new ObservableCollection<Account>();
        public static Account SelectedAccount;
        public static Friend SelectedFriend;
        public static Friends FriendsDataAccess = new Friends();

        public FriendsViewModel()
        {
            FriendsAccounts.Clear();
            DeleteCommand = new RelayCommand<Account>(DeleteFriendFromFrendsList);
            SortAllUsersCommand = new RelayCommand(SortUsers);
        }

        // Gets Accounts and Friends from database and adds to ObservableCollection
        internal async Task LoadAccountsAsync()
        {
            // Fills Accounts ObservableCollection
            var accounts = await CreateAccountViewModel.AccountDataAccess.GetAccountsAsync();

            foreach (var account in accounts)
            {
                if (account.UserId != LoginViewModel.LoggedInAccount.UserId)
                    Accounts.Add(account);
            }

            // Fills FriendsAccounts ObservableCollection
            var friends = await FriendsDataAccess.GetFriendsAsync();
            var friendsAccountsQuery = from friend in friends where friend.UserId == LoginViewModel.LoggedInAccount.UserId select friend.FriendId;
            
            foreach (int friendId in friendsAccountsQuery)
            {
                foreach (Account account in accounts)
                    if (account.UserId == friendId)
                        FriendsAccounts.Add(account);
            }

            // Fills Friends ObservableCollection
            var friendsQuery = from queryFriend in friends where queryFriend.UserId == LoginViewModel.LoggedInAccount.UserId select queryFriend;
            foreach (Friend dbFriend in friendsQuery)
                AccountViewModel.Friends.Add(dbFriend);
        }

        // Deletes friend from FriendsListView and Friend in database
        private static async void DeleteFriendFromFrendsList(Account friendAccount)
        {
            var friends = await FriendsDataAccess.GetFriendsAsync();
            var friendQuery = from aFriend in friends where aFriend.UserId == friendAccount.UserId select aFriend;

            foreach (Friend friendInQuery in friendQuery)
            {
                SelectedFriend = friendInQuery;
            }

            if (friendAccount == null)
                Misc.ShowToastNotification("Error", "No friend selected.", 1);
            else
            if (await FriendsDataAccess.DeleteFriendAsync(SelectedFriend))
            {
                FriendsAccounts.Remove(friendAccount);
                Misc.ShowToastNotification("Alert", friendAccount.Username + " deleted.", 1);
            }
        }

        // Sorts AccountsFriendsList
        private void SortUsers()
        {
            Debug.WriteLine("hmm");
            Sort(Accounts, account => account.FirstName);
        }

        // Sorts ObservableCollection Accounts
        public void Sort<TSource, TKey>(ObservableCollection<TSource> observableCollection, Func<TSource, TKey> keySelector)
        {
            Debug.WriteLine("3");
            var a = observableCollection.OrderBy(keySelector).ToList();
            observableCollection.Clear();
            foreach (var b in a)
            {
                observableCollection.Add(b);
                Debug.WriteLine("lool");
            }
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
