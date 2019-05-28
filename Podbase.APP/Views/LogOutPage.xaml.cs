using Windows.UI.Xaml.Controls;
using Podbase.APP.Services;

namespace Podbase.APP.Views
{
    public sealed partial class LogOutPage : Page
    {
        public static bool HideNavigation;

        public LogOutPage()
        {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame rootFrame = Windows.UI.Xaml.Window.Current.Content as Frame;
            NavigationService.Frame = rootFrame;
            NavigationService.Navigate<LoginPage>();
        }
    }
}
