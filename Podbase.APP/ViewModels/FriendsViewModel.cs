using System.Collections.ObjectModel;
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
        public ICommand AddToFriendsCommand { get; set; }
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public ObservableCollection<Account> FriendsAccounts { get; set; } = new ObservableCollection<Account>();
        public static ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        public static Friends FriendsDataAccess = new Friends();
        public static Account SelectedAccount;
        public static Friend SelectedFriend;

        public FriendsViewModel()
        {
            FriendsAccounts.Clear();
            SortAllUsersCommand = new RelayCommand<Account>(acc => PodcastViewModel.Sort(Accounts, account => account.FirstName));
            DeleteCommand = new RelayCommand<Account>(DeleteFriendFromFrendsList, account => account != null);

            AddToFriendsCommand = new RelayCommand<Account>(async account => {
                                                                Friend newFriend = new Friend() { UserId = LoginViewModel.LoggedInAccount.UserId, FriendId = account.UserId};

                                                                if (await FriendsDataAccess.AddFriendAsync(newFriend))
                                                                    Friends.Add(newFriend);
                                                                    FriendsAccounts.Add(account);
                                                            }, account => account != null & !FriendsAccounts.Contains(account));
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
                Friends.Add(dbFriend);
        }

        // Deletes friend from FriendsListView and Friend in database
        public async void DeleteFriendFromFrendsList(Account friendAccount)
        {
            var friends = await FriendsDataAccess.GetFriendsAsync();
            var friendQuery = from aFriend in friends where aFriend.UserId == LoginViewModel.LoggedInAccount.UserId && aFriend.FriendId == friendAccount.UserId select aFriend;

            foreach (var friend in friendQuery)
                SelectedFriend = friend;

            if (await FriendsDataAccess.DeleteFriendAsync(SelectedFriend))
            {
                FriendsAccounts.Remove(friendAccount);
                Misc.ShowToastNotification("Alert", friendAccount.Username + " deleted.", 1);
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
