using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class PingdbContext : IdentityDbContext<PingUser>
    {
        public PingdbContext() 
            : base("PingdbContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static PingdbContext Create()
        {
            return new PingdbContext();
        }
    }
}