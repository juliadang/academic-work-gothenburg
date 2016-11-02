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
    class PingReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Android.Content.Intent intent)
        {
            ((AppActivity)context).GetPings();
            InvokeAbortBroadcast();
        }
    }
}