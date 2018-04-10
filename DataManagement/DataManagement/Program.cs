using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var departments = Ice.PT_Department.ToList();

                    foreach (var d in departments)
                    {
                        Neo.Departments.Add(new Department()
                        {
                            Name = d.Department_Name,
                            SortOrder = d.Dept_Order,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            CreatedBy = "SYS",
                            IsActive = true,
                            UpdatedBy = "SYS"
                        });
                    }
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
