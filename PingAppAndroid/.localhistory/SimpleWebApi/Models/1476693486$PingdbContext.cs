using SimpleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class PingdbContext : DbContext
    {
        public PingdbContext() : base("PingdbContext")
        {
        }
        public DbSet<PingUser> PingUsers { get; set; }
    }
}