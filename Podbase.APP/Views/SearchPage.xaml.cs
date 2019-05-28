using Podbase.APP.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Podbase.APP.Views
{
    public sealed partial class SearchPage : Page
    {
        public SearchViewModel ViewModel { get; } = new SearchViewModel();

        public SearchPage()
        {
            InitializeComponent();
        }
    }
}
