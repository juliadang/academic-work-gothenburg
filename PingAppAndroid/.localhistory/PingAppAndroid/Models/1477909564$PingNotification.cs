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

namespace PingAppAndroid.Models
{
    public class PingNotification
    {
        public PingNotification(DateTime time, string sender, string receiver, int type)
        {
            IsRead = false;
            Time = time;
            Sender = sender;
            Receiver = receiver;
            Type = type;
        }
        public bool IsRead { get; set; }
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Type { get; set; }
    }
}