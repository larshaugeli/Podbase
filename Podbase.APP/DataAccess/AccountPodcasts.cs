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
    public class AccountPodcasts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri accountPodcastsBaseUri = new Uri("http://localhost:62289/api/accountpodcasts");

        public async Task<Podcast[]> GetPodcastsPerAccountAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(accountPodcastsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Podcast[] accountPodcasts = JsonConvert.DeserializeObject<Podcast[]>(json);

            return accountPodcasts;
        }

        internal async Task<bool> AddPodcastToPodcastListAsync(Podcast accountPodcasts)
        {
            string json = JsonConvert.SerializeObject(accountPodcasts);
            HttpResponseMessage result = await _httpClient.PostAsync(accountPodcastsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedPodcast = JsonConvert.DeserializeObject<Podcast>(json);
                accountPodcasts.PodcastId = returnedPodcast.PodcastId;

                return true;
            }
            else
                return false;
        }

        internal async Task<bool> DeletePodcastFromListAsync(Account account)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(accountPodcastsBaseUri, "podcasts/" + account.UserId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
