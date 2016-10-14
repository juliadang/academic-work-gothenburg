using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class ApplicationUserManager : UserManager<PingUser>
    {
        public ApplicationUserManager(IUserStore<PingUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<PingdbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<PingUser>(appDbContext));

            return appUserManager;
        }
    }
}