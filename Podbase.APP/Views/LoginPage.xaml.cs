using Windows.UI.Xaml.Controls;
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
            await ViewModel.LoadAccountsAsync();
        }
    }
}
