using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PingAppServer.Models
{
    public class PingUsers
    {
        [Table("PingUsers")]
        public class PingUsers
        {
            [Key]
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
        }
    }
}