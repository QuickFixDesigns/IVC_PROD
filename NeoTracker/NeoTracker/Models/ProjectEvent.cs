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
    public class ProjectEvent : EntityBase
    {
        public int ProjectEventID { get; set; }

        public int ProjectID { get; set; }

        public int? ProjectItemID { get; set; }

        public int? DepartmentID { get; set; }

        public int ProjectEventTypeID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Cannot be longer than 255 characters.")]
        public string Description { get; set; }

        //navigation
        public Project Project { get; set; }
        public Department Department { get; set; }
        public ProjectItem ProjectItem { get; set; }
        public ProjectEventType ProjectEventType { get; set; }

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
