using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podbase.APP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
