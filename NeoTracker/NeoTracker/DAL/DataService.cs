using NeoTracker.DAL;
using NeoTracker.Models;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.DAL
{
    public static class DataService
    {
        public static async Task<UserViewModel> GetUser(string Email)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Users.Include(x => x.DepartmentUsers).Include(x => x.DepartmentUsers.Select(d=>d.Department)).Where(x => x.Email == Email).Select(x => new UserViewModel()
                {
                    Alias = x.Alias,
                    CreatedAt = x.CreatedAt,
                    Email = Email,
                    Departments = x.DepartmentUsers.Select(d => new DepartmentViewModel()
                    {
                        DepartmentID = d.DepartmentID,
                        CreatedAt = d.Department.CreatedAt,
                        HeadOfDepartment = d.Department.HeadOfDepartment,
                        IsActive = d.Department.IsActive,
                        Name = d.Department.Name,
                        SortOrder = d.Department.SortOrder,
                        UpdatedAt = d.Department.UpdatedAt,
                        UpdatedBy = d.Department.UpdatedBy,
                    }).ToList(),
                    EmailNotifications = x.EmailNotifications,
                    FirstName = x.FirstName,
                    IsActive = x.IsActive,
                    IsAdmin = x.IsAdmin,
                    LastName = x.LastName,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UserID = x.UserID
                }).FirstOrDefaultAsync();
            }
        }
        public static async Task<List<DepartmentViewModel>> GetDepartmentList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Departments.Include(x => x.HeadOfDepartment).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new DepartmentViewModel()
                {
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CanManageProject =x.CanManageProject,
                    HeadOfDepartment = x.HeadOfDepartment,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }

        public static async Task<List<ProjectViewModel>> GetProjectList()
        {
            using (var genius = new IVCLIVEDBEntities())
            using (var context = new NeoTrackerContext())
            {
                return await context.Projects.Include(x => x.ProjectType).Include(x => x.Items).OrderBy(x => x.Name).Select(x => new ProjectViewModel()
                {
                    Code = x.Code,
                    Comment = x.Comment,
                    Name = x.Name,
                    Client = x.Client,
                    PurchaseOrder = x.PurchaseOrder,
                    ProjectID = x.ProjectID,
                    ProjectType = x.ProjectTypeID.HasValue ? new ProjectTypeViewModel()
                    {
                        ProjectTypeID = x.ProjectType.ProjectTypeID,
                        Name = x.ProjectType.Name,
                    } : null,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                }).ToListAsync();

            }
        }
        public static async Task<List<ProjectTypeViewModel>> GetProjectTypeList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectTypes.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new ProjectTypeViewModel()
                {
                    ProjectTypeID = x.ProjectTypeID,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public static async Task<List<EventTypeViewModel>> GetEventTypeList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.EventTypes.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new EventTypeViewModel()
                {
                    EventTypeID = x.EventTypeID,
                    Name = x.Name,
                    Notificate = x.Notificate,
                    IsDueDateChange = x.IsDueDateChange,
                    IsPriceChange = x.IsPriceChange,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public static async Task<List<StatusViewModel>> GetStatusList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Statuses.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new StatusViewModel()
                {
                    StatusID = x.StatusID,
                    SortOrder = x.SortOrder,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public static async Task<List<UserViewModel>> GetUserList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Select(x => new UserViewModel()
                {
                    UserID = x.UserID,
                    Alias = x.Alias,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    EmailNotifications = x.EmailNotifications,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
    }
}
