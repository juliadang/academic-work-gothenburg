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
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace PingAppAndroid.Models
{
    public class SignalRservice
    {
        HubConnection HubCon;
        IHubProxy PingProxy;
        public SignalRservice()
        {
            HubCon = new HubConnection("http://pinggothenburg.azurewebsites.net/signalr");
            PingProxy = HubCon.CreateHubProxy("PingHub");
        }

        public async Task<bool> StartSignalrConAsync()
        {
            await HubCon.Start();
            return true;
        }

        
    }
}