namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {

        public AccountViewModel()
        {

        }

        private string _username = LoginViewModel.loggedInUsername;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
    }
}
