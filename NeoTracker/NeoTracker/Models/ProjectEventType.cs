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
    public class EventType : EntityBase
    {
        public int EventTypeID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Name { get; set; }

        public int? SortOrder { get; set; }
        public bool NotificateDepartment { get; set; }

        public ICollection<Event> Events { get; set; }


        public EventTypeViewModel ToViewModel()
        {
            return new EventTypeViewModel()
            {
                EventTypeID = EventTypeID,
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
