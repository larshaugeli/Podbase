using System;
using Windows.UI.Xaml;
using Podbase.APP.ViewModels;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Podbase.APP.Helpers;

namespace Podbase.APP.Views
{
    public sealed partial class AccountPage : Page
    {
        public AccountViewModel ViewModel { get; } = new AccountViewModel();

        public AccountPage()
        {
            InitializeComponent();
            if (AccountViewModel.FromFriendsPage == false)
            {
                AboutMeTextBox.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;
                Loaded += AccountPage_LoadedAsync;
            }
            else
            {
                AboutMeTextBox.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;
                ToggleSwitch.Visibility = Visibility.Collapsed;
            }
        }

        private async void AccountPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try { 
                await ViewModel.LoadAccountsAsync();
            }
            catch (JsonReaderException)
            {
                await Misc.CreateMessageDialog("Error", "Could not read Json from database, please check your internet or database connection.");
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn)
                {
                    AboutMeTextBox.Visibility = Visibility.Visible;
                    AboutMeTextBlock.Visibility = Visibility.Collapsed;
                    SaveButton.Visibility = Visibility.Visible;
                }
                else
                {
                    AboutMeTextBox.Visibility = Visibility.Collapsed;
                    AboutMeTextBlock.Visibility = Visibility.Visible;
                    SaveButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleSwitch.IsOn = false;
        }
    }
}
