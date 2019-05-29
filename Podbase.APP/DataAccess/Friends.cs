using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Podbase.Model;

namespace Podbase.APP.DataAccess
{
    public class Friends
    {
        readonly HttpClient _httpClient = new HttpClient();
        private static readonly Uri FriendsBaseUri = new Uri("http://localhost:62289/api/friends");

        public async Task<Friend[]> GetFriendsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(FriendsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Friend[] friends = JsonConvert.DeserializeObject<Friend[]>(json);

            return friends;
        }

        internal async Task<bool> AddFriendAsync(Friend friend)
        {
            string json = JsonConvert.SerializeObject(friend);
            HttpResponseMessage result = await _httpClient.PostAsync(FriendsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedFriend = JsonConvert.DeserializeObject<Friend>(json);
                friend.ConnectionId = returnedFriend.ConnectionId;
                return true;
            }
            else
                return false;
        }

        internal async Task<bool> DeleteFriendAsync(Friend friend)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(FriendsBaseUri, "friends/" + friend.ConnectionId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
