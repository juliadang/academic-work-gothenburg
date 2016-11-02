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

namespace PingAppAndroid.Services
{
    class PingServiceConnection : Java.Lang.Object, IServiceConnection
    {
        StockActivity activity;

        public StockServiceConnection(StockActivity activity)
        {
            this.activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var stockServiceBinder = service as StockServiceBinder;
            if (stockServiceBinder != null)
            {
                var binder = (StockServiceBinder)service;
                activity.binder = binder;
                activity.isBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.isBound = false;
        }
    }
}
