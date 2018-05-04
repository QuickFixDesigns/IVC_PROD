using System;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class Operation : EntityBase
    {
        public int OperationID { get; set; }

        public int ItemID { get; set; }

        public int? SortOrder { get; set; }

        public int DepartmentID { get; set; }

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

        public bool IsCompleted { get; set; }

        public int? UserID { get; set; }

        //navigation
        public Item Item { get; set; }
        public Department Department { get; set; }
        public User User { get; set; }
    }
}
