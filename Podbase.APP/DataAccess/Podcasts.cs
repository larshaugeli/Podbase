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
    public class Podcasts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri podcastsBaseUri = new Uri("http://localhost:6289/api/podcasts");

        public async Task<Podcast[]> GetPodcastsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(podcastsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Podcast[] podcasts = JsonConvert.DeserializeObject<Podcast[]>(json);

            return podcasts;
        }

        internal async Task<bool> AddPodcastAsync(Podcast podcast)
        {
            string json = JsonConvert.SerializeObject(podcast);
            HttpResponseMessage result = await _httpClient.PostAsync(podcastsBaseUri,
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
                await _httpClient.DeleteAsync(new Uri(podcastsBaseUri, "podcasts/" + podcast.PodcastId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
