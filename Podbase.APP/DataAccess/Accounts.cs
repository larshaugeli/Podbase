using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Podbase.Model;

namespace Podbase.APP.DataAccess
{
    public class Accounts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri accountsBaseUri = new Uri("http://localhost:62289/api/accounts");

        public async Task<Account[]> GetAccountsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(accountsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Account[] accounts = JsonConvert.DeserializeObject<Account[]>(json);

            return accounts;
        }

        internal async Task<bool> AddAccountAsync(Account account)
        {
            string json = JsonConvert.SerializeObject(account);
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("json:" + json);
            HttpResponseMessage result = await _httpClient.PostAsync(accountsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("result: " + result.ToString());
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("resultcode: " + result.IsSuccessStatusCode);

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedAccount = JsonConvert.DeserializeObject<Account>(json);
                account.UserId = returnedAccount.UserId;
                Debug.WriteLine("Added to database");
                return true;
            }
            else
                Debug.WriteLine("Failed to add to database");
                return false;
        }

        internal async Task<bool> DeleteAccountAsync(Account account)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(accountsBaseUri, "accounts/" + account.UserId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
