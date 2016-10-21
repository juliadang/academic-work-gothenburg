using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SimpleWebApi
{
    public class PingHub : Hub
    {
        public void Hello(string val)
        {
            
            Clients.All.hey(val);
        }
    }
}