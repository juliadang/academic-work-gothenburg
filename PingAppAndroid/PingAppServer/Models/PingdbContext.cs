using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PingAppServer.Models
{
    public class PingdbContext : DbContext
    {
        public PingdbContext() : base("PingdbContext")
        {
        }
        public DbSet<PingUsers> PingUsers { get; set; }
    }
}