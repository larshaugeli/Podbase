using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Podbase.APP.Annotations;
using Podbase.APP.DataAccess;
using Podbase.APP.Helpers;
using Podbase.Model;

namespace Podbase.APP.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private Account account;

        public ICommand CreateAccountCommand { get; set; }
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        private Accounts accountsDataAccess = new Accounts();

        // SKJØNNER IKKE
        //public CreateAccountViewModel()
        //{
        //    CreateAccountCommand = new RelayCommand()
        //    {
        //        var account = new Account()
        //        {
        //        };
        //        if (await accountsDataAccess.AddAccountAsync(account))
        //            Accounts.Add(account);
        //    }
        //}

        //internal async Task LoadAccountsAsync()
        //{
        //    var accounts = await accountsDataAccess.GetAccountsAsync();
        //    foreach (Account account in accounts)
        //        Accounts.Add(account);
        //}
    }
}
