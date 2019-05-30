using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.Helpers;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public ICommand SaveTextCommand { get; set; }
        public static ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        public static string LoggedInAboutMe;
        public static bool FromFriendsPage { get; set; }

        private string _username, _firstName, _lastName, _aboutMe;
        
        public AccountViewModel()
        {
            // If AccountPage is opened by double clicking on a user in FriendPage
            if (FromFriendsPage)
            {
                // sets TextBlocks to be the selected user from FriendPage
                _username = FriendsViewModel.SelectedAccount.Username;
                _firstName = FriendsViewModel.SelectedAccount.FirstName;
                _lastName = FriendsViewModel.SelectedAccount.LastName;
                _aboutMe = FriendsViewModel.SelectedAccount.AboutMe;
            }
            // If AccountPage is opened from anywhere but when double click on a user in FriendPage
            else
            {
                SaveTextCommand = new RelayCommand(SaveText);
                CreateAccountViewModel.Accounts.Clear();

                // sets TextBlocks to be the logged in user
                _username = LoginViewModel.LoggedInAccount.Username;
                _firstName = LoginViewModel.LoggedInAccount.FirstName;
                _lastName = LoginViewModel.LoggedInAccount.LastName;
                _aboutMe = LoggedInAboutMe;
            }
        }

        // Sets edited AboutMe TextBlock
        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.AccountDataAccess.GetAccountsAsync();
            var query = from acc in accounts where acc.UserId == LoginViewModel.LoggedInAccount.UserId select acc;
            foreach (Account account in query)
                LoggedInAboutMe = account.AboutMe;

            var friends = await FriendsViewModel.FriendsDataAccess.GetFriendsAsync();
            foreach (Friend friend in friends)
                Friends.Add(friend);
        }

        // Saves text from AboutMe TextBox to database and displays it in AboutMe TextBlock
        public async void SaveText()
        {
            LoginViewModel.LoggedInAccount.AboutMe = AboutMe;

            // Modifies table row in database
            using (var db = new PodbaseContext(Misc.OptionsBuilder().Options))
            {
                var result = db.Accounts.SingleOrDefault(b => b.UserId == LoginViewModel.LoggedInAccount.UserId);
                if (result != null)
                {
                    CreateAccountViewModel.Accounts.Add(LoginViewModel.LoggedInAccount);
                    result.AboutMe = AboutMe;
                    LoggedInAboutMe = AboutMe;
                    db.SaveChanges();
                }
            }

            Account[] accounts = await CreateAccountViewModel.AccountDataAccess.GetAccountsAsync();
            var query = from acc in accounts where acc.UserId == LoginViewModel.LoggedInAccount.UserId select acc;
            foreach (Account account in query)
            {
                account.AboutMe = LoggedInAboutMe;
            }
        }

        // Input strings
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

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

        public string AboutMe
        {
            get => _aboutMe;
            set
            {
                _aboutMe = value;
                OnPropertyChanged("AboutMe");
            }
        }
    }
}
