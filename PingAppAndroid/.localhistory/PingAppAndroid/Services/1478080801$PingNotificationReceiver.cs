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
    [BroadcastReceiver]
    [IntentFilter(new string[] { PingService.PingUpdatedAction }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class PingNotificationReceiver : BroadcastReceiver
    {
        public PingNotificationReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var nMgr = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, "You got a new ping");
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(AppActivity)), 0);
            //notification.SetLatestEventInfo(context, "Ping Received", "Check your pings", pendingIntent);
            nMgr.Notify(0, notification);
        }
    }
}