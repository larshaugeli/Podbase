using System;
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
        static readonly Uri AccountsBaseUri = new Uri("http://localhost:62289/api/accounts");

        public async Task<Account[]> GetAccountsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(AccountsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Account[] accounts = JsonConvert.DeserializeObject<Account[]>(json);

            return accounts;
        }

        internal async Task<bool> AddAccountAsync(Account account)
        {
            string json = JsonConvert.SerializeObject(account);
            HttpResponseMessage result = await _httpClient.PostAsync(AccountsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedAccount = JsonConvert.DeserializeObject<Account>(json);
                account.UserId = returnedAccount.UserId;
                return true;
            }
            else
                return false;
        }

        internal async Task<bool> DeleteAccountAsync(Account account)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(AccountsBaseUri, "accounts/" + account.UserId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
