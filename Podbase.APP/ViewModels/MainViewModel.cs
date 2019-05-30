using System;
using System.Collections.Generic;
using Podbase.APP.Helpers;

namespace Podbase.APP.ViewModels
{
    /// <summary>
    /// This view model contain code relevant to the home page.
    /// This class displays text on the home page.
    /// </summary>
    public class MainViewModel : Observable
    {
        public static List<string> QuotesList = new List<string>();
        private string _username = LoginViewModel.LoggedInAccount.Username;
        private string _quote;

        public MainViewModel()
        {
            _quote = ChoseQuote(new Random().Next(1,5));
        }

        private static string ChoseQuote(int random)
        {
            QuotesList.Add("The greatest glory in living lies not in never falling, but in rising every time we fall. - Nelson Mandela");
            QuotesList.Add("The way to get started is to quit talking and begin doing. - Walt Disney");
            QuotesList.Add("Spread love everywhere you go.Let no one ever come to you without leaving happier. - Mother Teresa");
            QuotesList.Add("The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt");
            QuotesList.Add("No, I'm not going to tell them about the downsizing. If a patient has cancer, you don't tell them. - Michael Scott");
            QuotesList.Add("Would I rather be feared or loved? Easy. Both. I want people to be afraid of how much they love me. - Michael Scott");
            return QuotesList[random];
        }

        public string Username
        {
            get => "Welcome " + _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string QuoteOfTheDay
        {
            get => _quote;
            set
            {
                _quote = value;
                OnPropertyChanged("QuoteOfTheDay");
            }
        }
    }
}
