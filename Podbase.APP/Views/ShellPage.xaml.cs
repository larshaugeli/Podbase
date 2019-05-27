using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Podbase.APP.Helpers;

namespace Podbase.APP.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }
    }
}
