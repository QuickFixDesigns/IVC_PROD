using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.DAL
{
    public static class GetProductionCheckList
    {
        public static List<DepartmentOperation> GetList()
        {

            return new List<DepartmentOperation>()
            {
                new DepartmentOperation()
                {
                    SortOrder = 1,
                    IsActive = true,
                    Name = "3D",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 2,
                    IsActive = true,
                    Name = "Mise en plan",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 3,
                    IsActive = true,
                    Name = "Top sheet au client",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 4,
                    IsActive = true,
                    Name = "PDF",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 5,
                    IsActive = true,
                    Name = "Kit LED",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 6,
                    IsActive = true,
                    Name = "Kit metal",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 7,
                    IsActive = true,
                    Name = "Kit paint",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 8,
                    IsActive = true,
                    Name = "DXF",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 9,
                    IsActive = true,
                    Name = "Shipping/Box/PAD/Sac plastique",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 10,
                    IsActive = true,
                    Name = "Genius",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 11,
                    IsActive = true,
                    Name = "Approbation",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 12,
                    IsActive = true,
                    Name = "Die lines",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 13,
                    IsActive = true,
                    Name = "Revision",
                    OperationTime = 0
                },
                new DepartmentOperation()
                {
                    SortOrder = 14,
                    IsActive = true,
                    Name = "Plan de montage",
                    OperationTime = 0
                },
            };
        }
    }
}
