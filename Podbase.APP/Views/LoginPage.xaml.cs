using System;
using Windows.ApplicationModel.Core;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Podbase.APP.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; } = new LoginViewModel();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void CreateAccount_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccount));
        }
    }
}
