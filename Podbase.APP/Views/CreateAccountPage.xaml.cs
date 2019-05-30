using System;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Podbase.APP.Helpers;
using Podbase.APP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podbase.APP.Views
{
    public sealed partial class CreateAccount : Page
    {
        public CreateAccountViewModel ViewModel { get; } = new CreateAccountViewModel();

        public CreateAccount()
        {
            InitializeComponent();
            DataContext = ViewModel;
            Loaded += CreateAccounts_LoadedAsync;
        }

        private async void CreateAccounts_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
