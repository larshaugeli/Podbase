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
    public class Friends
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri friendsBaseUri = new Uri("http://localhost:62289/api/friends");

        public async Task<Friend[]> GetFriendsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(friendsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Friend[] friends = JsonConvert.DeserializeObject<Friend[]>(json);

            return friends;
        }

        internal async Task<bool> AddFriendAsync(Friend friend)
        {
            string json = JsonConvert.SerializeObject(friend);
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("json:" + json);
            HttpResponseMessage result = await _httpClient.PostAsync(friendsBaseUri,
                new StringContent(json, Encoding.UTF8, "application/json"));
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("result: " + result.ToString());
            Debug.WriteLine("______________________________________________");
            Debug.WriteLine("resultcode: " + result.IsSuccessStatusCode);

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedFriend = JsonConvert.DeserializeObject<Friend>(json);
                friend.UserId = returnedFriend.UserId;
                Debug.WriteLine("Added to database");
                return true;
            }
            else
                Debug.WriteLine("Failed to add to database");
                return false;
        }

        internal async Task<bool> DeleteFriendAsync(Friend friend)
        {
            HttpResponseMessage result =
                await _httpClient.DeleteAsync(new Uri(friendsBaseUri, "friends/" + friend.UserId.ToString()));
            return result.IsSuccessStatusCode;
        }
    }
}
