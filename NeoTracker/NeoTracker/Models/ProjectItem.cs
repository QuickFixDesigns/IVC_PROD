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
    public class ProjectItem : EntityBase
    {
        public int ProjectItemID { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Code { get; set; }

        public int SortOrder { get; set; }

        public int StatusID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? LatestStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public Status Status { get; set; }
        public ICollection<ProjectItemOperation> ProjectItemOperations { get; set; }
    }
}
