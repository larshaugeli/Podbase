using System;
using System.Diagnostics;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Podbase.APP.Helpers;

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
            try
            {
                await AccountViewModel.LoadAccountsAsync();
            }
            catch (JsonReaderException)
            {
                await Misc.CreateMessageDialog("Error",
                    "Could not read Json from database, please check your internet or database connection.");
            }
        }
    }
}
