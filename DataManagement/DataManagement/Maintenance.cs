using DataManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public static class Maintenance
    {
        public static void ResetDb()
        {
            Console.WriteLine("Maintenance.ResetDb");
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    //project
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Event ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Operation ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Item ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Project ");

                    //admin
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM DepartmentUSer ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM DepartmentOperation ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM [User] ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Department ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM EventType ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM Status ");
                    Neo.Database.ExecuteSqlCommand(" DELETE  FROM ProjectType ");

                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
