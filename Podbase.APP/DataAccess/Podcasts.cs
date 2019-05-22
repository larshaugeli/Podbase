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
    public class Podcasts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri podcastsBaseUri = new Uri("http://localhost:62289/api/podcasts");

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
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("json:" + json);
            HttpResponseMessage result = await _httpClient.PostAsync(podcastsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("result: " + result.ToString());
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("resultcode: " + result.IsSuccessStatusCode);


            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedPodcast = JsonConvert.DeserializeObject<Podcast>(json);
                podcast.PodcastId = returnedPodcast.PodcastId;
                Debug.WriteLine("Added to database");
                return true;
            }
            else
                Debug.WriteLine("Failed to add to database");
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
