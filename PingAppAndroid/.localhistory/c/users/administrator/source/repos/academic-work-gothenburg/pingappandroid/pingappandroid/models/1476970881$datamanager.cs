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
        static ISharedPreferences prefs = Application.Context.GetSharedPreferences("token", FileCreationMode.Private);
        static ISharedPreferencesEditor editor = prefs.Edit();
        static HttpClient client = new HttpClient();

        static List<string> friendlist;

        static public bool Register(string url)
        {

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.Method = "GET";
            return true;

            //// Send the request to the server and wait for the response:
            using (WebResponse response = request.GetResponse()) { }
            //{
            //    // Get a stream representation of the HTTP web response:
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        // Use this stream to build a JSON document object:
            //        JsonValue jsonDoc = await Task.Run(() => JSONObject.Load(stream));
            //        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //        // Return the JSON document:
            //        return jsonDoc;
            //    }
            //}
        }

        internal static async void GetAllFriends()
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/getfriendlist/";
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", prefs.GetString("token", ""));

            var response = await client.GetAsync(uri);

            //if (response.IsSuccessStatusCode)
            //{
            var jsonFriendlist = await response.Content.ReadAsStringAsync();
            friendlist = JsonConvert.DeserializeObject<List<string>>(jsonFriendlist);
            //    return friendlist;
            //}
            //else
            //    return "Connection failed";
        }

        internal static async Task<string> AddFriend(string username2)
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/add/";
            api += username2;
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", prefs.GetString("token", ""));

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);

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

            response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string receive = await response.Content.ReadAsStringAsync();
                var jwt = JsonConvert.DeserializeObject<JWTObj>(receive);
                editor.PutString("token", jwt.Access_token);
                editor.Apply();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}