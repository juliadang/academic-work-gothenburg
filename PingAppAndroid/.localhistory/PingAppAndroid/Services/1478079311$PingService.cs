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
using Newtonsoft.Json;

namespace PingAppAndroid.Services
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.PingService" })]
    public class PingService : IntentService
    {
        IBinder binder;
        List<PingNotification> mPingList;
        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("token", FileCreationMode.Private);
        public const string PingUpdatedAction = "Ping received";

        protected override void OnHandleIntent(Intent intent)
        {
            //var stockSymbols = new List<string>() { "AMZN", "FB", "GOOG", "AAPL", "MSFT", "IBM" };

            mPingList = UpdatePings();

            var pingIntent = new Intent(PingUpdatedAction);

            SendOrderedBroadcast(pingIntent, null);
        }

        private List<PingNotification> UpdatePings()
        {
            HttpClient mClient = new HttpClient();
            string api = "http://pinggothenburg.azurewebsites.net/api/accounts/getpings/";
            var uri = new Uri(api);

            var content = new StringContent("", Encoding.UTF8);
            mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mPrefs.GetString("token", "")); //get from shared preferences

            var response = mClient.GetAsync(uri);

            var jsonPings = response.Content.ReadAsStringAsync();
            mPingList = JsonConvert.DeserializeObject<List<PingNotification>>(jsonPings);

            return mPingList;
        }
        public override IBinder OnBind(Intent intent)
        {
            binder = new PingServiceBinder(this);
            return binder;
        }


        public List<PingNotification> GetPings()
        {
            return mPingList;
        }
    }
}
