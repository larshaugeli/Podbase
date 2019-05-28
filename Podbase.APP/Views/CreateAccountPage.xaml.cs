using Windows.UI.Xaml.Controls;
using Podbase.APP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podbase.APP.Views
{
    public sealed partial class CreateAccount : Page
    {
        public CreateAccountViewModel ViewModel { get; } = new CreateAccountViewModel();

        public CreateAccount()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
