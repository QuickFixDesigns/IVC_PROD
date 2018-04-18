using DataManagement.InitNeoTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class Admin
    {
        public static void LoadDataBase()
        {
            LoadDepartments();
            LoadUsers();
            LoadUserDepartments();
            LoadProjectType();
            LoadStatus();
            LoadEventTypes();
        }

        public static void LoadDepartments()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var list = Ice.PT_Department.ToList();

                    foreach (var i in list)
                    {
                        Department d = new Department()
                        {
                            Name = i.Department_Name,
                            SortOrder = i.Dept_Order,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            CreatedBy = "SYS",
                            IsActive = true,
                            UpdatedBy = "SYS"
                        };
                        if (i.Department_Name.Equals("Production"))
                        {
                            d.DepartmentOperations = GetLists.GetDepartmentOperations();
                        }
                        Neo.Departments.Add(d);
                    }
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void LoadUsers()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var list = Ice.PT_User.ToList();

                    foreach (var i in list)
                    {
                        Neo.Users.Add(new User()
                        {
                            FirstName = i.First_Name,
                            LastName = i.Last_Name,
                            Email = i.Email_Address,
                            EmailNotifications = i.Receive_Via_Email == "YES",
                            IsAdmin = false,
                            Alias = i.PT_User1,
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

        public static void LoadUserDepartments()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                using (var Ice = new Ice_Project_TrackerEntities())
                {
                    var list = (from d in Ice.PT_User_Department
                                join department in Ice.PT_Department on d.Department_Key equals department.Department_Key
                                select new
                                {
                                    department.Dept_Order,
                                    d.PT_User
                                }).ToList();

                    foreach (var i in list)
                    {
                        var department = Neo.Departments.FirstOrDefault(x => x.SortOrder == i.Dept_Order);
                        var user = Neo.Users.FirstOrDefault(x => x.Alias == i.PT_User);

                        if (department != null && user != null)
                        {
                            Neo.DepartmentUsers.Add(new DepartmentUser()
                            {
                                DepartmentID = department.DepartmentID,
                                UserID = user.UserID,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                CreatedBy = "SYS",
                                IsActive = true,
                                UpdatedBy = "SYS"
                            });
                        }
                    }
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static void LoadStatus()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    var list = GetLists.GetStatus();
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
                    var list = GetLists.GetProjectTypes();
                    Neo.ProjectTypes.AddRange(list);
                    Neo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static void LoadEventTypes()
        {
            try
            {
                using (var Neo = new NeoTrackerDbEntities())
                {
                    var list = GetLists.GetEventTypes();
                    Neo.EventTypes.AddRange(list);
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
