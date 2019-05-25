using System;

using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Podbase.APP.Views
{
    public sealed partial class FriendsPage : Page
    {
        public FriendsViewModel ViewModel { get; } = new FriendsViewModel();

        public FriendsPage()
        {
            InitializeComponent();
            Loaded += PodcastPage_LoadedAsync;
        }

        private async void PodcastPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadAccountsAsync();
        }
    }
}
