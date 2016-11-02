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
using PingAppAndroid.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PingAppAndroid.Services
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.PingService" })]
    public class PingService : IntentService
    {
        IBinder binder;
        List<PingNotification> pings;
        static HttpClient mClient = new HttpClient();

        public const string PingUpdatedAction = "Ping received";

        protected override void OnHandleIntent(Intent intent)
        {
            //var stockSymbols = new List<string>() { "AMZN", "FB", "GOOG", "AAPL", "MSFT", "IBM" };

            pings = UpdatePings();

            var stocksIntent = new Intent(StocksUpdatedAction);

            SendOrderedBroadcast(stocksIntent, null);
        }

        private List<PingNotification> UpdatePings()
        {
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/getpings/";
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", "")); //get from shared preferences

            var response = await mClient.GetAsync(uri);

            var jsonPings = await response.Content.ReadAsStringAsync();
            mPingList = JsonConvert.DeserializeObject<List<PingNotification>>(jsonPings);
        }
    }
    }
}