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
    public class Event : EntityBase
    {
        public int EventID { get; set; }

        public int ProjectID { get; set; }

        public int? ItemID { get; set; }

        public int? DepartmentID { get; set; }

        public int EventTypeID { get; set; }

        [Required]
        public string Description { get; set; }

        //navigation
        public Project Project { get; set; }
        public Department Department { get; set; }
        public Item Item { get; set; }
        public EventType EventType { get; set; }

    }
}
