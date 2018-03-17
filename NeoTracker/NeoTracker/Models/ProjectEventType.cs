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
    public class ProjectEventType : EntityBase
    {
        public int ProjectEventTypeID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Name { get; set; }

        public int? SortOrder { get; set; }
        public bool NotificateDepartment { get; set; }

        public ICollection<ProjectEvent> ProjectEvents { get; set; }


        public ProjectEventTypeViewModel ToViewModel()
        {
            return new ProjectEventTypeViewModel()
            {
                ProjectEventTypeID = ProjectEventTypeID,
                Name = Name,
                NotificateDepartment = NotificateDepartment,
                SortOrder = SortOrder,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy,
            };
        }
    }
}
