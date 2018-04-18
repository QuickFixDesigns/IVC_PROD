namespace NeoTracker.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.NeoTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NeoTracker.DAL.NeoTrackerContext";
        }

        protected override void Seed(DAL.NeoTrackerContext context)
        {
        }
    }
}
