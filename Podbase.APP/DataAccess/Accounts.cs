using System;
using System.Collections.Generic;
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
        static readonly Uri accountsBaseUri = new Uri("http://localhost:6289/api/accounts");

        public async Task<Account[]> GetActorsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(accountsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Account[] accounts = JsonConvert.DeserializeObject<Account[]>(json);

            return accounts;
        }

        internal async Task<bool> AddAccountAsync(Account account)
        {
            string json = JsonConvert.SerializeObject(account);
            HttpResponseMessage result = await _httpClient.PostAsync(accountsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedAccount = JsonConvert.DeserializeObject<Account>(json);
                account.LoginId = returnedAccount.LoginId;

                return true;
            }
            else
                return false;
        }

        internal async Task<bool> DeleteActorAsync(Account account)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(accountsBaseUri, "accounts/" + account.LoginId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
