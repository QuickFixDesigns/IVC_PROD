using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class ProjectItemOperation : EntityBase
    {
        public int ProjectItemOperationID { get; set; }

        public int ProjectItemID { get; set; }

        public int? SortOrder { get; set; }

        public int? DepartmentID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:P0}")]
        public decimal OperationTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:P0}")]
        public decimal Progress { get; set; }

        //navigation
        public ProjectItem ProjectItem { get; set; }
        public Department Department { get; set; }

        //public DepartmentViewModel ToViewModel()
        //{
        //    return new DepartmentViewModel()
        //    {
        //        DepartmentID = DepartmentID,
        //        HeadOfDepartmentID = HeadOfDepartmentID,
        //        Name = Name,
        //        SortOrder = SortOrder,
        //        Msg = Msg,
        //        IsDefault = IsDefault,
        //        CreatedAt = CreatedAt,
        //        UpdatedAt = UpdatedAt,
        //        UpdatedBy = UpdatedBy,
        //    };
        //}
    }
}
