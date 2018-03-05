using NeoTracker.Models;
using System.Data.Entity;

namespace NeoTracker.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<NeoTrackerContext>
    {
        protected override void Seed(NeoTrackerContext context)
        {
            context.Users.Add(new User()
            {
                Alias = "K",
                Email = "Karrick_Mercier@hotmail.com",
                EmailNotifications = true,
                FirstName = "Karrick",
                IsActive = true,
                IsAdmin = true,
                LastName = "Mercier",
            });
            context.SaveChanges();
         
        }
    }
}