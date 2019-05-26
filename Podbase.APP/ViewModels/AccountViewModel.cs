﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.EntityFrameworkCore;
using Podbase.APP.Helpers;
using Podbase.DataAccess;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public RelayCommand SaveTextCommand { get; set; }
        public Account LoggedInAccount;
        public static string LoggedInAboutMe, FriendUsername, FriendFirstName, FriendLastName, FriendAboutMe;
        public static int FriendUserID;
        public static bool FromFriendsPage { get; set; } = false;
        private string _username, _firstName, _lastName, _aboutMe;

        public AccountViewModel()
        {
            if (FromFriendsPage == false)
            {
                _username = LoginViewModel.loggedInUsername;
                _firstName = LoginViewModel.loggedInFirstName;
                _lastName = LoginViewModel.loggedInLastName;
                _aboutMe = LoggedInAboutMe;

                CreateAccountViewModel.Accounts.Clear();
                SaveTextCommand = new RelayCommand(SaveText);
            }
            else
            {
                _username = FriendsViewModel.SelectedAccount.Username;
                _firstName = FriendsViewModel.SelectedAccount.FirstName;
                _lastName = FriendsViewModel.SelectedAccount.LastName;
                _aboutMe = FriendsViewModel.SelectedAccount.AboutMe;
            }
        }

        internal async Task LoadAccountsAsync()
        {
            var accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            var query = from acc in accounts where acc.UserId == LoginViewModel.loggedInUserId select acc;
            foreach (Account account in query)
            {
                LoggedInAboutMe = account.AboutMe;
                Debug.WriteLine("AboutMe: " + LoggedInAboutMe + " og " + "account.AboutMe " + account.AboutMe);
            }
        }

        public async void SaveText()
        {
            Account[] accounts = await CreateAccountViewModel.accountDataAccess.GetAccountsAsync();
            foreach (Account acc in accounts)
            {
                if (acc.UserId == LoginViewModel.loggedInUserId)
                {
                    LoggedInAccount = acc;
                    Debug.WriteLine("LoggedInAccount is set.");
                }
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
            
            var optionsBuilder = new DbContextOptionsBuilder<PodbaseContext>();
            optionsBuilder.UseSqlServer(Misc.StringBuilder());

            // modifies table row
            using (var db = new PodbaseContext(optionsBuilder.Options))
            {
                var result = db.Accounts.SingleOrDefault(b => b.UserId == LoginViewModel.loggedInUserId);
                Debug.WriteLine("LoggedIAccount UserId: " + LoggedInAccount.UserId + " " + "loggedINUSerID: " + " " + LoginViewModel.loggedInUserId);
                if (result != null)
                {
                    CreateAccountViewModel.Accounts.Add(LoggedInAccount);
                    result.AboutMe = AboutMe;
                    LoggedInAboutMe = AboutMe;
                    Debug.WriteLine("results: LoggedInAboutMe " + LoggedInAboutMe + " results.AboutMe " + result.AboutMe);
                    db.SaveChanges();
                }
            }

            var query = from acc in accounts where acc.UserId == LoginViewModel.loggedInUserId select acc;
            foreach (Account account in query)
            {
                account.AboutMe = LoggedInAboutMe;
                Debug.WriteLine("AboutMe: " + AboutMe + " og " + "account.AboutMe " + account.AboutMe);
            }
        }

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
