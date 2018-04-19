using DataManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.InitNeoTracker
{
    public static class GetLists
    {
        public static List<DepartmentOperation> GetDepartmentOperations()
        {
            return new List<DepartmentOperation>()
            {
                new DepartmentOperation()
                {
                    SortOrder = 1,
                    IsActive = true,
                    Name = "3D",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 2,
                    IsActive = true,
                    Name = "Mise en plan",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 3,
                    IsActive = true,
                    Name = "Top sheet au client",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 4,
                    IsActive = true,
                    Name = "PDF",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 5,
                    IsActive = true,
                    Name = "Kit LED",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 6,
                    IsActive = true,
                    Name = "Kit metal",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 7,
                    IsActive = true,
                    Name = "Kit paint",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 8,
                    IsActive = true,
                    Name = "DXF",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 9,
                    IsActive = true,
                    Name = "Shipping/Box/PAD/Sac plastique",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 10,
                    IsActive = true,
                    Name = "Genius",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 11,
                    IsActive = true,
                    Name = "Approbation",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 12,
                    IsActive = true,
                    Name = "Die lines",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 13,
                    IsActive = true,
                    Name = "Revision",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new DepartmentOperation()
                {
                    SortOrder = 14,
                    IsActive = true,
                    Name = "Plan de montage",
                    OperationTime = 0,
                    CreatedBy = "SYS",
                    UpdatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
            };
        }
        public static List<Status> GetStatus()
        {
            return new List<Status>()
            {
                new Status()
                {
                    Name = "Created",
                    IsDeleted = false,
                    IsActive = true,
                    SortOrder =1,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new Status()
                {
                    Name = "Completed",
                    IsDeleted = false,
                    IsActive = true,
                    SortOrder =2,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
            };
        }
        public static List<ProjectType> GetProjectTypes()
        {
            return new List<ProjectType>()
            {
                new ProjectType()
                {
                    Name = "Open",
                    IsActive = true,
                    SortOrder =1,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new ProjectType()
                {
                    Name = "Closed",
                    IsActive = true,
                    SortOrder =2,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
            };
        }
        public static List<EventType> GetEventTypes()
        {
            return new List<EventType>()
            {
                new EventType()
                {
                    Name = "Note",
                    IsDueDateChange = false,
                    IsPriceChange = false,
                    Notificate = false,
                    IsActive = true,
                    SortOrder =1,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new EventType()
                {
                    Name = "Fast track",
                    IsDueDateChange = false,
                    IsPriceChange = false,
                    Notificate = true,
                    IsActive = true,
                    SortOrder =2,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new EventType()
                {
                    Name = "Price change",
                    IsDueDateChange = false,
                    IsPriceChange = true,
                    Notificate = false,
                    IsActive = true,
                    SortOrder =2,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new EventType()
                {
                    Name = "Date change",
                    IsDueDateChange = true,
                    IsPriceChange = false,
                    Notificate = false,
                    IsActive = true,
                    SortOrder =3,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new EventType()
                {
                    Name = "Date and price change",
                    IsDueDateChange = true,
                    IsPriceChange = true,
                    Notificate = false,
                    IsActive = true,
                    SortOrder =4,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
                new EventType()
                {
                    Name = "Problem",
                    IsDueDateChange = false,
                    IsPriceChange = false,
                    Notificate = true,
                    IsActive = true,
                    SortOrder =5,
                    CreatedBy = "SYS",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "SYS",
                },
            };
        }
    }
}
