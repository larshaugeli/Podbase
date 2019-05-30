﻿using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podbase.APP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChosenPodcastPage : Page
    {
        public ChosenPodcastViewModel ViewModel { get; } = new ChosenPodcastViewModel();

        public ChosenPodcastPage()
        {
            this.InitializeComponent();

        }
    }
}
