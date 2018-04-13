using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class InitProjectAndItem
    {
        public static void LoadStatus()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    var list = new List<Status>()
                    {
                        new Status()
                        {
                            Name = "Created",
                            IsDeleted = false,
                            IsActive = true,
                            SortOrder =1,
                            CreatedBy = "sys",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = "sys",
                        },
                        new Status()
                        {
                            Name = "Completed",
                            IsDeleted = false,
                            IsActive = true,
                            SortOrder =1,
                            CreatedBy = "sys",
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = "sys",
                        },
                    };
                    Neo.Status.AddRange(list);
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
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
                        Project p = new Project()
                        {
                            Name = i.Project_Name,
                            Comment = i.Notes,
                            Code = i.Project_Tracker_Number,
                            ProjectTypeID = Neo.ProjectTypes.FirstOrDefault(x => x.Name == i.Open_Or_Closed).ProjectTypeID,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            CreatedBy = "SYS",
                            IsActive = true,
                            UpdatedBy = "SYS"
                        };
                        GetProjectItems(p);
                        Neo.Projects.Add(p);
                    }
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static void GetProjectItems(Project Project)
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Genius = new IVCLIVEDBEntities())
                {
                    var StatusCreatedID = Neo.Status.First(x => x.Name == "Created").StatusID;
                    var StatusCompletedID = Neo.Status.First(x => x.Name == "Completed").StatusID;

                    var list = Genius.Comm2.Where(x => x.No_Com == Project.Code).Select(x => new Item()
                    {
                        Code = x.Item,
                        DueDate = x.Dateliv,
                        IsActive = true,
                        Name = x.Des,
                        SortKey = x.Clef,
                        SortOrder = x.Ligneitm,
                        StatusID = x.DateClient < DateTime.Now ? StatusCompletedID : StatusCreatedID,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedBy = "SYS",
                        UpdatedBy = "SYS"
                    }).ToList();

                    list.ForEach(x => GetItemOperations(x, Project.Code));
                    Project.Items = list;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static void GetItemOperations(Item item, string Order)
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var departments = Neo.Departments.ToList();

                    var temp = (from o in Ice.PT_Project_SalesOrder_Part.Where(x => x.Sales_Order_Key == Order && x.Part.Equals(item.Code))
                                join d in Ice.PT_Department on o.Department_Key equals d.Department_Key
                                select new { o, d }
                    ).ToList();

                    var list = temp.Select(x => new Operation()
                    {
                        DepartmentID = departments.First(y => y.SortOrder == x.d.Dept_Order).DepartmentID,
                        Name = x.d.Department_Name,
                        SortOrder = x.d.Dept_Order,
                        EndDate = DateTime.ParseExact(x.o.End_Date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture),
                        IsActive = true,
                        IsCompleted = x.o.Completed_Yes_No.Equals("Yes"),
                        OperationTime = x.o.Plan_Hrs,
                        StartDate = DateTime.ParseExact(x.o.Start_Date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedBy = "SYS",
                        UpdatedBy = "SYS",
                    });
                    item.Operations = list.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
