using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Infrastructure
{
    public class Notifications
    {
        public Notification(DateTime time, string sender, string receiver, int type)
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