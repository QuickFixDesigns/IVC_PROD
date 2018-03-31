using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class DepartmentOperation : EntityBase
    {
        public int DepartmentOperationID { get; set; }

        public int? SortOrder { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:P0}")]
        public decimal OperationTime { get; set; }

        public int DepartmentID { get; set; }

        //navigation
        public Department Department { get; set; }
    }
}
