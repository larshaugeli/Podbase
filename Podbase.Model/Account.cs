using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Podbase.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static List<Account> Accounts { get; set; } = new List<Account>();

        // password must be minimum 4 characters, minimum 1 upper case letter and minimum 1 number
        public static Regex ValidPassword = new Regex("(?=.*[A-Z])(?=.*[0-9])(?=.{4,})");

        public static bool UsernameNotTaken(string username)
        {
            if (Accounts.Count == 0)
                return true;

            foreach (Account account in Accounts)
            {
                if (username.Equals(account.Username))
                    return true;
            }

            return false;
        }
    }
}
