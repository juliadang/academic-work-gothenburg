namespace SimpleWebApi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleWebApi.Models.PingdbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleWebApi.Models.PingdbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<PingUser>(new UserStore<PingUser>(new PingdbContext()));

            var user = new PingUser()
            {
                Username = "SuperPowerUser",
            };

            manager.Create(user, "hejsan123");
        }
    }
}
