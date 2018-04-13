using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class Maintenance
    {
        public static void ResetDb()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Operation ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Item ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Project ");
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
