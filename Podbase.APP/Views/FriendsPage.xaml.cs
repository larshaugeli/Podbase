using System;
using System.Diagnostics;
using Podbase.APP.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Newtonsoft.Json;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.Views
{
    public sealed partial class FriendsPage : Page
    {
        public FriendsViewModel ViewModel { get; } = new FriendsViewModel();

        public FriendsPage()
        {
            InitializeComponent();
            Loaded += FriendsPage_LoadedAsync;
            AccountsListView.ItemsSource = ViewModel.Accounts;
            SortAllUsers.Command = ViewModel.SortAllUsersCommand;
            AddToFriends.Command = ViewModel.AddToFriendsCommand;
            FriendsListView.ItemsSource = ViewModel.FriendsAccounts;
        }

        private async void FriendsPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                await ViewModel.LoadAccountsAsync();
            }
            catch (JsonReaderException)
            {
                await Misc.CreateMessageDialog("Error",
                    "Could not read Json from database, please check your internet or database connection.");
            }
        }

        private void UIElement_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (((Grid) sender).DataContext is Account selectedAccount)
            {
                ViewModel.GoToSelectedAccount(selectedAccount);
            }
        }
    }
}
