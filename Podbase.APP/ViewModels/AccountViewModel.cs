namespace Podbase.APP.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {

        }

        private string _username = LoginViewModel.loggedInUsername;
        private string _firstName = LoginViewModel.loggedInFirstName;
        private string _lastName = LoginViewModel.loggedInLastName;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
    }
}
