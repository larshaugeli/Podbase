using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

namespace Podbase.APP.Views
{
    public sealed partial class ChosenPodcastPage : Page
    {
        public ChosenPodcastViewModel ViewModel { get; } = new ChosenPodcastViewModel();

        public ChosenPodcastPage()
        {
            this.InitializeComponent();

        }
    }
}
