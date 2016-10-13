using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi
{
    public class PingHub : Hub
    {
        public void Hello()
        {
            Clients.All.SayHi("Hello!");
        }
    }
}