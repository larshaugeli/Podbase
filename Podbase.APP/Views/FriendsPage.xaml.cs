using System;

using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Podbase.APP.Services;

namespace Podbase.APP.Views
{
    public sealed partial class FriendsPage : Page
    {
        public FriendsViewModel ViewModel { get; } = new FriendsViewModel();

        public FriendsPage()
        {
            InitializeComponent();
            Loaded += FriendsPage_LoadedAsync;
        }

        private async void FriendsPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadAccountsAsync();
        }

        private void AccountsListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationService.Navigate(typeof(AccountPage));
        }
    }
}
