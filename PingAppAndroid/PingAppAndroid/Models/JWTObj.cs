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
    class JWTObj
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
    }
}