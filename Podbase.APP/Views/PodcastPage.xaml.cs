using System;
using Podbase.APP.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Newtonsoft.Json;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.Views
{
    public sealed partial class PodcastPage : Page
    {
        public PodcastViewModel ViewModel { get; } = new PodcastViewModel();

        public PodcastPage()
        {
            InitializeComponent();
            Loaded += PodcastPage_LoadedAsync;
            PodcastsListView.ItemsSource = ViewModel.Podcasts;
        }

        private async void PodcastPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                await ViewModel.LoadPodcastsAsync();
            }
            catch (JsonReaderException)
            {
                await Misc.CreateMessageDialog("Error",
                    "Could not read Json from database, please check your internet or database connection.");
            }
        }

        private void UIElement_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (((Grid)sender).DataContext is Podcast selectedPodcast)
            {
                ViewModel.GoToSelectedPodcast(selectedPodcast);
            }
        }
    }
}
