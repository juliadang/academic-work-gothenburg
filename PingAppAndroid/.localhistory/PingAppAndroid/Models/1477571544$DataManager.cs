using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;

using System.Threading.Tasks;
using System.Net.Http;

using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PingAppAndroid.Models
{
    static public class DataManager
    {
        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("token", FileCreationMode.Private);
        static ISharedPreferencesEditor mEditor = mPrefs.Edit();
        static HttpClient mClient = new HttpClient();
        static List<string> mFriendlist;

        //Get friends from datamanager

        internal static List<string> GetAllFriends()
        {
            return mFriendlist;
        }

        //Get friends from database
        internal static async Task<bool> GetAllFriendsAsync()
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/getfriendlist/";
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", "")); //Save to shared preferences

            var response = await mClient.GetAsync(uri);

            var jsonFriendlist = await response.Content.ReadAsStringAsync();
            return true;
            //mFriendlist = JsonConvert.DeserializeObject<List<string>>(jsonFriendlist);
        }

        internal static async Task<string> AddFriendAsync(string username2)
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/add/";
            api += username2;
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", ""));

            HttpResponseMessage response = null;

            response = await mClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return "Connection fail";
        }

        internal static async Task<bool> SignInAsync(string userName, string password)
        {
            var uri = new Uri("http://pinggothenburg.azurewebsites.net/oauth/token");
            //var uri = new Uri("http://localhost:11014/oauth/token");

            var grantString = "grant_type=password&username=" + userName + "&password=" + password;
            var content = new StringContent(grantString, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = null;

            response = await mClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                var jwt = JsonConvert.DeserializeObject<JWTObj>(token);
                mEditor.PutString("token", jwt.Access_token);
                mEditor.Apply();
                return true;
            }
            else
                return false;
        }
    }
}