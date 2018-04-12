using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class InitProjectAndItem
    {
        public static void LoadProjectType()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    var list = new List<ProjectType>()
                    {
                        new ProjectType()
                        {
                            Name = "Open",
                            IsActive = true,
                            SortOrder =1,
                            CreatedBy = "sys",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = "sys",
                        },
                        new ProjectType()
                        {
                            Name = "Closed",
                            IsActive = true,
                            SortOrder =2,
                            CreatedBy = "sys",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = "sys",
                        }
                    };
                    Neo.ProjectTypes.AddRange(list);
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void LoadProjects()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var list = Ice.PT_Master.ToList();

                    foreach (var i in list)
                    {
                        Neo.Projects.Add(new Project()
                        {
                            Name = i.Project_Name,
                            Comment = i.Notes,
                            Code = i.Project_Tracker_Number,
                            ProjectTypeID = Neo.ProjectTypes.FirstOrDefault(x=>x.Name == i.Open_Or_Closed).ProjectTypeID,
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
