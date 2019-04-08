using System;

using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Podbase.APP.Views
{
    public sealed partial class AccountPage : Page
    {
        public AccountViewModel ViewModel { get; } = new AccountViewModel();

        public AccountPage()
        {
            InitializeComponent();
        }
    }
}
