using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Org.Json;
using System.Net.Http;
using SimpleWebApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        internal static async void GetAllFriendsAsync()
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/getfriendlist/";
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", ""));

            var response = await mClient.GetAsync(uri);

            var jsonFriendlist = await response.Content.ReadAsStringAsync();
            mFriendlist = JsonConvert.DeserializeObject<List<string>>(jsonFriendlist);
        }

        internal static async Task<string> AddFriend(string username2)
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/add/";
            api += username2;
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", ""));

            HttpResponseMessage response = null;

            response = await mClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var test = await response.Content.ReadAsStringAsync();
                var test1 = test.ToString();
                return test;
            }
            else
            {
                return "Connection fail";
            }
        }

        internal static async Task<bool> SignIn(string userName, string password)
        {
            var uri = new Uri("http://pinggothenburg.azurewebsites.net/oauth/token");
            //var uri = new Uri("http://localhost:11014/oauth/token");

            var grantString = "grant_type=password&username=" + userName + "&password=" + password;
            var content = new StringContent(grantString, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = null;

            response = await mClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string receive = await response.Content.ReadAsStringAsync();
                var jwt = JsonConvert.DeserializeObject<JWTObj>(receive);
                mEditor.PutString("token", jwt.Access_token);
                mEditor.Apply();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}