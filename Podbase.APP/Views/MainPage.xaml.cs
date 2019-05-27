using System;
using System.Diagnostics;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Podbase.APP.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        public AccountViewModel AccountViewModel { get; } = new AccountViewModel();

        public MainPage()
        {
            InitializeComponent();

            Loaded += AccountPage_LoadedAsync;
        }

        private async void AccountPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await AccountViewModel.LoadAccountsAsync();
            Debug.WriteLine("async method happened");
        }
    }
}
