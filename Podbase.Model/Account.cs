using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Podbase.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string Json { get; set; }

        public Account()
        {
            Username = this.Username;
            UserId = this.UserId;
            Password = this.Password;
            FirstName = this.FirstName;
            LastName = this.LastName;
            AboutMe = this.AboutMe;
        }

        public Account(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jUserId = ["user"];
            UserId = (int)jObject["userId"];
            Username = (string)jObject["username"];
            Password = (string)jObject["password"];
            FirstName = (string)jObject["firstName"];
            LastName = (string) jObject["lastName"];
            AboutMe = (string) jObject["aboutMe"];
        }

        public string ConvertFromObjectToJson()
        {
            Json = @"[{""userId"":1,""username"":""hey"",""password"":""Q123"",""firstName"":""hmm"",""lastName"":""oi"",""aboutMe"":null}]";
            return Json;
        }

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
