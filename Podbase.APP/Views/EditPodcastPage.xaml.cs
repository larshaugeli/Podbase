using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

namespace Podbase.APP.Views
{
    public sealed partial class EditPodcastPage : Page
    {
        public EditPodcastViewModel ViewModel { get; } = new EditPodcastViewModel();

        public EditPodcastPage()
        {
            InitializeComponent();
        }
    }
}
