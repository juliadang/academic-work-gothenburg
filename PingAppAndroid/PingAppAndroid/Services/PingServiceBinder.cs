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
    public class PingServiceBinder : Binder
    {
        PingService service;

        public PingServiceBinder(PingService service)
        {
            this.service = service;
        }

        public PingService GetPingService()
        {
            return service;
        }
    }
}