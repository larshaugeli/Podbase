using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Podbase.APP.Helpers;
using Podbase.APP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
            catch (HttpRequestException exception)
            {
                Misc.CreateDialog("Error", exception.Message);
            }
        }
    }
}
