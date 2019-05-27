using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Podbase.APP.ViewModels;

using Windows.UI.Xaml.Controls;
using Podbase.APP.DataAccess;
using Podbase.APP.Services;
using NavigationViewPaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode;

namespace Podbase.APP.Views
{
    public sealed partial class LogOutPage : Page
    {
        public LogOutPage()
        {
            InitializeComponent();
        }
    }
}
