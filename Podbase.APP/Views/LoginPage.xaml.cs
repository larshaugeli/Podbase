using System;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Podbase.APP.Helpers;
using Podbase.APP.ViewModels;

namespace Podbase.APP.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; } = new LoginViewModel();

        public LoginPage()
        {
            InitializeComponent();
            Loaded += LoginPage_LoadedAsync;            
        }

        private async void LoginPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                await ViewModel.LoadAccountsAsync();
            }
            catch (JsonReaderException)
            {
                await Misc.CreateMessageDialog("Error", "Could not read Json from database, please check your internet or database connection.");
            }
        }
    }
}
