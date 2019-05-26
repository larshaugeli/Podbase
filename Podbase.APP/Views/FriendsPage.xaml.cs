﻿using System;
using System.Diagnostics;
using System.Linq;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Podbase.APP.Services;
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
            YourFriends.ItemsSource = FriendsViewModel.FriendsAccounts;
        }

        private async void FriendsPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadAccountsAsync();
        }

        private void UIElement_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Account selectedAccount = ((Grid) sender).DataContext as Account;
            if (selectedAccount == null) Debug.WriteLine("no item selected");
            ViewModel.GoToSelectedAccount(selectedAccount);
        }
    }
}
