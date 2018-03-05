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
    public class Project : EntityBase
    {
        public int ProjectID { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Code { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string OrderNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Range(0, 10)]
        public int? Priority { get; set; }

        [StringLength(255, ErrorMessage = "Cannot be longer than 255 characters.")]
        public string Comment { get; set; }

        //navigation
        public ICollection<ProjectEvent> ProjectEvents { get; set; }
        public ICollection<ProjectItem> ProjectItems { get; set; }

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
