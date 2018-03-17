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
    public class DataService
    {
        public async Task<User> GetUser(string Email)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Email == Email);
            }
        }
        public async Task<List<DepartmentViewModel>> GetDepartmentList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Departments.Include(x => x.HeadOfDepartment).OrderBy(x => x.Name).Select(x => new DepartmentViewModel()
                {
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    HeadOfDepartment = x.HeadOfDepartment,
                    Msg = x.Msg,
                    IsDefault = x.IsDefault,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }

        public async Task<List<ProjectViewModel>> GetProjectList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Projects.Include(x => x.ProjectItems).Select(x => new ProjectViewModel()
                {
                    Code = x.Code,
                    Comment = x.Comment,
                    Name = x.Name,
                    Priority = x.Priority,
                    ProjectID = x.ProjectID,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                }).ToListAsync();
            }
        }

        public async Task<List<OrderViewModel>> GetOrderList()
        {
            using (var context = new IVCLIVEDBEntities())
            {
                return await context.Comms.Where(x => x.Datecli > DateTime.Today).Select(x => new OrderViewModel()
                {
                    Code = x.No_Com,
                    Date = x.Datecli,
                    Po = x.No_Po,
                }).ToListAsync();
            }
        }
        public async Task<List<ProjectEventViewModel>> GetProjectEventList(int? ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectEvents.Include(x => x.Project).Include(x => x.ProjectItem).Include(x => x.ProjectEventType).Include(x => x.Department).OrderByDescending(x => x.CreatedAt).Select(x => new ProjectEventViewModel()
                {
                    ProjectEventID = x.ProjectEventID,
                    Department = x.DepartmentID.HasValue ? new DepartmentViewModel()
                    {
                        CreatedAt = x.Department.CreatedAt,
                        DepartmentID = x.Department.DepartmentID,
                        //HeadOfDepartment = x.Department.HeadOfDepartment,
                        IsActive = x.Department.IsActive,
                        IsDefault = x.Department.IsDefault,
                        Msg = x.Department.Msg,
                        Name = x.Department.Name,
                        SortOrder = x.Department.SortOrder,
                        UpdatedAt = x.Department.UpdatedAt,
                        UpdatedBy = x.Department.UpdatedBy,
                    } : null,
                    Description = x.Description,
                    Project = x.Project,
                    ProjectEventType = new ProjectEventTypeViewModel()
                    {
                        ProjectEventTypeID = x.ProjectEventTypeID,
                        Name = x.ProjectEventType.Name,
                        NotificateDepartment = x.ProjectEventType.NotificateDepartment,
                        SortOrder = x.ProjectEventType.SortOrder,
                        IsActive = x.ProjectEventType.IsActive,
                        CreatedAt = x.ProjectEventType.CreatedAt,
                        UpdatedAt = x.ProjectEventType.UpdatedAt,
                        UpdatedBy = x.ProjectEventType.UpdatedBy,
                    },
                    ProjectItem = x.ProjectItemID.HasValue ? new ProjectItemViewModel()
                    {
                        IsActive = x.ProjectItem.IsActive,
                        Code = x.ProjectItem.Code,
                        CreatedAt = x.ProjectItem.CreatedAt,
                        UpdatedBy = x.ProjectItem.UpdatedBy,
                        UpdatedAt = x.ProjectItem.UpdatedAt,
                        SortOrder = x.ProjectItem.SortOrder,
                        DueDate = x.ProjectItem.DueDate,
                        LatestStartDate = x.ProjectItem.LatestStartDate,
                        Name = x.ProjectItem.Name,
                        //Project = x.ProjectItem.Project,
                        ProjectItemID = x.ProjectItem.ProjectItemID,
                        Status = x.ProjectItem.Status,
                    } : null,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<ProjectEventTypeViewModel>> GetProjectEventTypeList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectEventTypes.OrderBy(x => x.Name).Select(x => new ProjectEventTypeViewModel()
                {
                    ProjectEventTypeID = x.ProjectEventTypeID,
                    Name = x.Name,
                    NotificateDepartment = x.NotificateDepartment,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<ProjectItemViewModel>> GetProjectItemList(int? ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectItems.Include(x => x.Project).Include(x => x.Status).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new ProjectItemViewModel()
                {
                    ProjectItemID = x.ProjectItemID,
                    Code = x.Code,
                    DueDate = x.DueDate,
                    LatestStartDate = x.LatestStartDate,
                    Status = x.Status,
                    Project = x.Project,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<StatusViewModel>> GetStatusList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Statuses.OrderBy(x => x.Name).Select(x => new StatusViewModel()
                {
                    StatusID = x.StatusID,
                    SortOrder = x.SortOrder,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<UserViewModel>> GetUserList()
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
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
    }
}
