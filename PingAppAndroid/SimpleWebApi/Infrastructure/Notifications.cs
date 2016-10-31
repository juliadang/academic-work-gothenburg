using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Infrastructure
{
    public class Notifications
    {
        public Notifications()
        {

        }
        public Notifications(DateTime time, string sender, string receiver, int type)
        {
            IsRead = false;
            Time = time;
            Sender = sender;
            Receiver = receiver;
            Type = type;
        }
        [Key]
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Type { get; set; }
    }
}