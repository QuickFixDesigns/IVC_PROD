using System;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class Event : EntityBase
    {
        public int EventID { get; set; }

        public int ProjectID { get; set; }

        public int? ItemID { get; set; }

        public int? DepartmentID { get; set; }

        public int EventTypeID { get; set; }

        public int StatusID { get; set; }

        [Required]
        public string Description { get; set; }

        //navigation
        public Project Project { get; set; }
        public Department Department { get; set; }
        public Item Item { get; set; }
        public EventType EventType { get; set; }
        public Status Status { get; set; }

    }
}
