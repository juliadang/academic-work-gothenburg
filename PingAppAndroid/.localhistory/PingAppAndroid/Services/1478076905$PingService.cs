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

namespace PingAppAndroid.Services
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.PingService" })]
    public class PingService : IntentService
    {
        IBinder binder;
        List<PingNotification> pings;
        public const string PingUpdatedAction = "Ping received";

        protected override void OnHandleIntent(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}