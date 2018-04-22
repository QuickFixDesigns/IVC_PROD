namespace NeoTracker.Migrations
{
    using DAL;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NeoTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NeoTracker.DAL.NeoTrackerContext";
        }

        protected override void Seed(NeoTrackerContext context)
        {
            //context.Users.Add(new User()
            //{
            //    Alias = "K",
            //    Email = "Karrick_Mercier@hotmail.com",
            //    EmailNotifications = true,
            //    FirstName = "Karrick",
            //    IsActive = true,
            //    IsAdmin = true,
            //    LastName = "Mercier",
            //});
            //context.SaveChanges();
        }
    }
}
