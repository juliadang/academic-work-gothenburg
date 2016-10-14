namespace SimpleWebApi.Migrations
{
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleWebApi.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleWebApi.Infrastructure.ApplicationDbContext context)
        {
            var managar = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = new ApplicationUser();

            managar.Create(user, "hejsan123");
            #region Some tips
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            // 
            #endregion
        }
    }
}
