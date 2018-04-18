using NeoTracker.Models;
using System.Data.Entity;

namespace NeoTracker.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<NeoTrackerContext>
    {
        protected override void Seed(NeoTrackerContext context)
        {         
        }
    }
}