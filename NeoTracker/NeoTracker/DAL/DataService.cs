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
                var data = await context.Departments.Include(x => x.HeadOfDepartment).OrderBy(x => x.Name).ToListAsync();
                return data.Select(x => new DepartmentViewModel()
                {
                    DepartmentID = x.DepartmentID,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    HeadOfDepartmentID = x.HeadOfDepartmentID,
                    HeadOfDepartmentName = x.HeadOfDepartmentID.HasValue ? x.HeadOfDepartment.LongName : App.BlankStr,
                    Msg = x.Msg,
                    IsDefault = x.IsDefault,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToList();
            }
        }
        public async Task<List<ProjectEventTypeViewModel>> GetProjectEventTypeList()
        {
            using (var context = new NeoTrackerContext())
            {
                var data = await context.ProjectEventTypes.OrderBy(x => x.Name).ToListAsync();
                return data.Select(x => new ProjectEventTypeViewModel()
                {
                    ProjectEventTypeID = x.ProjectEventTypeID,
                    Name = x.Name,
                    NotificateDepartment = x.NotificateDepartment,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToList();
            }
        }
        public async Task<List<StatusViewModel>> GetStatusList()
        {
            using (var context = new NeoTrackerContext())
            {
                var data = await context.Statuses.OrderBy(x => x.Name).ToListAsync();
                return data.Select(x => new StatusViewModel()
                {
                    StatusID = x.StatusID,
                    SortOrder= x.SortOrder,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToList();
            }
        }
        public async Task<List<UserViewModel>> GetUserList()
        {
            using (var context = new NeoTrackerContext())
            {
                var data = await context.Users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToListAsync();
                return data.Select(x => new UserViewModel()
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
                }).ToList();
            }
        }
    }
}
