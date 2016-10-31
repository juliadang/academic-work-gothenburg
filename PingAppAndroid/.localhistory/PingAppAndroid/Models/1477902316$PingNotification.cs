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
    //Todo: Byta namn på denna klass eftersom android har en klass med samma namn(?)
    class PingNotification
    {
        public DateTime Time { get; set; }
        public Friend Sender { get; set; }
        public Friend Reciever { get; set; }
        public int Type { get; set; }
        public string ImgSource { get; set; }
    }
}