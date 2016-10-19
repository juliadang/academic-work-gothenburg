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

namespace PingAppAndroid.Models
{
    static public class DataManager
    {
        static HttpClient client = new HttpClient();

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

        internal static async Task<bool> AddFriend(string username2)
        {
           // var uri = new Uri("http://pinggothenburg.azurewebsites.net/api/accounts/addfriend");
            var uri = new Uri("http://localhost:11014/api/accounts/addfriend");

            var content = new StringContent(username2, Encoding.UTF8);

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;



            }
        }

        internal static async Task<bool> SignIn(string userName, string password)
        {
            //var uri = new Uri("http://pinggothenburg.azurewebsites.net/oauth/token");
            var uri = new Uri("http://localhost:11014/oauth/token");

            var grantString = "grant_type=password&username=" + userName + "&password=" + password;
            var content = new StringContent(grantString, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}