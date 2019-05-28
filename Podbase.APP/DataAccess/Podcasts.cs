using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Podbase.Model;

namespace Podbase.APP.DataAccess
{
    public class Podcasts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri PodcastsBaseUri = new Uri("http://localhost:62289/api/podcasts");

        public async Task<Podcast[]> GetPodcastsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(PodcastsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Podcast[] podcasts = JsonConvert.DeserializeObject<Podcast[]>(json);

            return podcasts;
        }

        internal async Task<bool> AddPodcastAsync(Podcast podcast)
        {
            string json = JsonConvert.SerializeObject(podcast);
            HttpResponseMessage result = await _httpClient.PostAsync(PodcastsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedPodcast = JsonConvert.DeserializeObject<Podcast>(json);
                podcast.PodcastId = returnedPodcast.PodcastId;
                return true;
            }
            else
                return false;
        }

        internal async Task<bool> DeletePodcastAsync(Podcast podcast)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(PodcastsBaseUri, "podcasts/" + podcast.PodcastId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
