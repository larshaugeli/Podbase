using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;

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
            await ViewModel.LoadAccountsAsync();
        }
    }
}
