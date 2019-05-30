using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

namespace Podbase.APP.Views
{
    public sealed partial class AddPodcastPage : Page
    {
        public AddPodcastViewModel ViewModel { get; } = new AddPodcastViewModel();

        public AddPodcastPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
