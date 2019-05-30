using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Podbase.Model
{
    public class Account
    {
        private string _username, _password;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username
        {
            get => _username;
            set => _username = value ?? throw new ArgumentException("Username cannot be null");
        }

        public string Password
        {
            get => _password;
            set
            {
                if (IsValid(value))
                    _password = value;
                else throw new ArgumentException("Password must contain minimum 4 characters, 1 upper case letter and 1 number.");
                
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public static List<Account> Accounts { get; set; } = new List<Account>();

        // password must be minimum 4 characters, minimum 1 upper case letter and minimum 1 number
        public static Regex ValidPassword = new Regex("(?=.*[A-Z])(?=.*[0-9])(?=.{4,})");

        public bool IsValid(string password)
        {
            return ValidPassword.IsMatch(password);
        }

        public bool UsernameTaken(string username)
        {
            if (Accounts.Count == 0)
                return false;

            foreach (Account account in Accounts)
            {
                if (username.Equals(account.Username))
                    return true;
            }

            return false;
        }
    }
}
