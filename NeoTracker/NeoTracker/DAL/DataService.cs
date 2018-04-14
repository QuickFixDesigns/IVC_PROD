﻿using NeoTracker.DAL;
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
        public async Task<UserViewModel> GetUser(string Email)
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
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UserID = x.UserID
                }).FirstOrDefaultAsync();
            }
        }
        public async Task<List<DepartmentViewModel>> GetDepartmentList()
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
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                }).ToListAsync();
            }
        }

        public async Task<string> GetOperationCount(int itemID)
        {
            //string count = Operations != null ? string.Concat(Operations.Count(x => x.IsCompleted).ToString(), " / ", Operations.Count.ToString()) : App.BlankStr;
            using (var context = new NeoTrackerContext())
            {
                int completed = await context.Operations.CountAsync(x => x.ItemID == itemID && x.IsCompleted);
                int total = await context.Operations.CountAsync(x => x.ItemID == itemID);

                return total != 0 ? string.Concat(completed.ToString(), " / ", total.ToString()) : App.BlankStr;
            }
        }

        public async Task<List<OrderViewModel>> GetOrderList()
        {
            using (var context = new NeoTrackerContext())
            using (var Genius = new IVCLIVEDBEntities())
            {
                var projects = context.Projects.Select(x => x.Code).ToArray();

                return await Genius.Comms.Where(x => x.Datecli > DateTime.Today && !projects.Contains(x.No_Com)).OrderByDescending(x => x.Datecli).ThenBy(x => x.No_Com).Select(x => new OrderViewModel()
                {
                    Code = x.No_Com,
                    Client = x.Fact_A1,
                    Date = x.Datecli,
                    Po = x.No_Po,
                }).ToListAsync();
            }
        }
        public async Task<List<EventViewModel>> GetEventList(int? ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Events.Where(x => !ProjectID.HasValue || x.ProjectID == ProjectID.Value).Include(x => x.Status).Include(x => x.Item).Include(x => x.EventType).Include(x => x.Department).OrderByDescending(x => x.CreatedAt).Select(x => new EventViewModel()
                {
                    EventID = x.EventID,
                    Department = x.DepartmentID.HasValue ? new DepartmentViewModel()
                    {
                        //CreatedAt = x.Department.CreatedAt,
                        DepartmentID = x.Department.DepartmentID,
                        //HeadOfDepartment = x.Department.HeadOfDepartment,
                        //IsActive = x.Department.IsActive,
                        //IsDefault = x.Department.IsDefault,
                        //Msg = x.Department.Msg,
                        Name = x.Department.Name,
                        //SortOrder = x.Department.SortOrder,
                        //UpdatedAt = x.Department.UpdatedAt,
                        //UpdatedBy = x.Department.UpdatedBy,
                    } : null,
                    Description = x.Description,
                    ProjectID = x.ProjectID,
                    EventType = new EventTypeViewModel()
                    {
                        EventTypeID = x.EventTypeID,
                        Name = x.EventType.Name,
                        //Notificate = x.EventType.Notificate,
                        //SortOrder = x.EventType.SortOrder,
                        //IsActive = x.EventType.IsActive,
                        //CreatedAt = x.EventType.CreatedAt,
                        //UpdatedAt = x.EventType.UpdatedAt,
                        //UpdatedBy = x.EventType.UpdatedBy,
                    },
                    EventItem = x.ItemID.HasValue ? new ItemViewModel()
                    {
                        //IsActive = x.Item.IsActive,
                        Code = x.Item.Code,
                        //CreatedAt = x.Item.CreatedAt,
                        //UpdatedBy = x.Item.UpdatedBy,
                        //UpdatedAt = x.Item.UpdatedAt,
                        //SortOrder = x.Item.SortOrder,
                        //DueDate = x.Item.DueDate,
                        //LatestStartDate = x.Item.LatestStartDate,
                        Name = x.Item.Name,
                        //Project = x.Item.Project,
                        ItemID = x.Item.ItemID,
                        Status = new StatusViewModel()
                        {
                            //CreatedAt = x.Item.Status.CreatedAt,
                            //IsActive = x.Item.Status.IsActive,
                            //IsDeleted = x.Item.Status.IsDeleted,
                            Name = x.Item.Status.Name,
                            //SortOrder = x.Item.Status.SortOrder,
                            StatusID = x.Item.Status.StatusID,
                            //UpdatedAt = x.Item.Status.UpdatedAt,
                            //UpdatedBy = x.Item.Status.UpdatedBy,
                        },
                    } : null,
                    Status = new StatusViewModel()
                    {
                        //CreatedAt = x.Item.Status.CreatedAt,
                        //IsActive = x.Item.Status.IsActive,
                        //IsDeleted = x.Item.Status.IsDeleted,
                        Name = x.Status.Name,
                        //SortOrder = x.Item.Status.SortOrder,
                        StatusID = x.Status.StatusID,
                        //UpdatedAt = x.Item.Status.UpdatedAt,
                        //UpdatedBy = x.Item.Status.UpdatedBy,
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<ProjectTypeViewModel>> GetProjectTypeList()
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectTypes.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new ProjectTypeViewModel()
                {
                    ProjectTypeID = x.ProjectTypeID,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<EventTypeViewModel>> GetEventTypeList()
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
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<ItemViewModel>> GetItemList(int? ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Items.Where(x => !ProjectID.HasValue || x.ProjectID == ProjectID.Value).Include(x => x.Project).Include(x => x.Operations).Include(x => x.Status).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new ItemViewModel()
                {
                    ItemID = x.ItemID,
                    Code = x.Code,
                    DueDate = x.DueDate,
                    EndDate = x.EndDate,
                    Status = new StatusViewModel()
                    {
                        //CreatedAt = x.Status.CreatedAt,
                        //IsActive = x.Status.IsActive,
                        //IsDeleted = x.Status.IsDeleted,
                        Name = x.Status.Name,
                        //SortOrder = x.Status.SortOrder,
                        StatusID = x.Status.StatusID,
                        //UpdatedAt = x.Status.UpdatedAt,
                        //UpdatedBy = x.Status.UpdatedBy,
                    },
                    ProjectID = x.ProjectID,
                    Name = x.Name,
                    Operations = x.Operations.Select(o => new OperationViewModel()
                    {
                        IsCompleted = o.IsCompleted
                    }).ToList(),
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<OperationViewModel>> GetOperationList(int ItemID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.Operations.Where(x => x.ItemID == ItemID).Include(x => x.Department).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new OperationViewModel()
                {
                    Department = x.Department,
                    EndDate = x.EndDate,
                    ItemID = x.ItemID,
                    OperationID = x.OperationID,
                    OperationTime = x.OperationTime,
                    IsCompleted = x.IsCompleted,
                    StartDate = x.StartDate,
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
                return await context.Statuses.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new StatusViewModel()
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
