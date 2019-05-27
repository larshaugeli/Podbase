using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.Helpers;
using Podbase.APP.Services;
using Podbase.APP.Views;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        // Variables
        // Commands
        public RelayCommand SaveTextCommand { get; set; }
        public ICommand AddFriendCommand { get; set; }

        //
        public static ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        public Account LoggedInAccount;
        public static bool FromFriendsPage { get; set; }

        // Strings
        private string _username, _firstName, _lastName, _aboutMe;
        public static string LoggedInAboutMe;
        

        public AccountViewModel()
        {
            // If AccountPage is opened from anywhere but when double click on a user in FriendPage
            if (FromFriendsPage == false)
            {
                SaveTextCommand = new RelayCommand(SaveText);
                CreateAccountViewModel.Accounts.Clear();

                // sets TextBlocks to be the logged in user
                _username = LoginViewModel.loggedInUsername;
                _firstName = LoginViewModel.loggedInFirstName;
                _lastName = LoginViewModel.loggedInLastName;
                _aboutMe = LoggedInAboutMe;
            }
            // If AccountPage is opened by double clicking on a user in FriendPage
            else
            {
                AddFriendCommand = new RelayCommand(AddFriend);
                // sets TextBlocks to be the selected user from FriendPage
                _username = FriendsViewModel.SelectedAccount.Username;
                _firstName = FriendsViewModel.SelectedAccount.FirstName;
                _lastName = FriendsViewModel.SelectedAccount.LastName;
                _aboutMe = FriendsViewModel.SelectedAccount.AboutMe;
            }
        }

        // Sets edited AboutMe TextBlock
        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            var query = from acc in accounts where acc.UserId == LoginViewModel.loggedInUserId select acc;
            foreach (Account account in query)
                LoggedInAboutMe = account.AboutMe;
        }

        // Adds a user as friend with another user. Adds the user to a ObservableCollection and to database
        private void AddFriend()
        {
            Friend selectedFriend = new Friend() { UserId = LoginViewModel.loggedInUserId, FriendId = FriendsViewModel.SelectedAccount.UserId };
            bool alreadyInFriends = Friends.Any(x => x.UserId == selectedFriend.UserId && x.FriendId == selectedFriend.FriendId);

            if (alreadyInFriends)
                Misc.ShowToastNotification("Notification", "You are already friend with " + FriendsViewModel.SelectedAccount.Username, 1);
            else
            {
                // Adds selected friend to ObservableCollection
                Friends.Add(selectedFriend);
                // Adds selected friend to database
                using (var db = new PodbaseContext(Misc.OptionsBuilder().Options))
                {
                    db.Friends.Add(selectedFriend);
                    db.SaveChanges();
                }

                Misc.ShowToastNotification("Notification","Added " + FriendsViewModel.SelectedAccount.Username + " as friend", 1);
                NavigationService.Navigate(typeof(FriendsPage));
                FromFriendsPage = false;
            }
        }

        // Saves text from AboutMe TextBox to database and displays it in AboutMe TextBlock
        public async void SaveText()
        {
            Account[] accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            foreach (Account acc in accounts)
            {
                if (acc.UserId == LoginViewModel.loggedInUserId)
                    LoggedInAccount = acc;
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

            // Modifies table row
            using (var db = new PodbaseContext(Misc.OptionsBuilder().Options))
            {
                var result = db.Accounts.SingleOrDefault(b => b.UserId == LoginViewModel.loggedInUserId);
                if (result != null)
                {
                    CreateAccountViewModel.Accounts.Add(LoggedInAccount);
                    result.AboutMe = AboutMe;
                    LoggedInAboutMe = AboutMe;
                    db.SaveChanges();
                }
            }

            var query = from acc in accounts where acc.UserId == LoginViewModel.loggedInUserId select acc;
            foreach (Account account in query)
            {
                account.AboutMe = LoggedInAboutMe;
            }
        }

        // Strings
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
    
        //public static void WriteFriendsInDebug()
        //{
        //    Debug.WriteLine("Friends Observable");
        //    foreach (var friend in Friends)
        //    {
        //        Debug.WriteLine("ConnectionId: " + friend.ConnectionId + "UserId: " + friend.UserId + " FriendId: " + friend.FriendId);
        //    }
        //}
    }
}
