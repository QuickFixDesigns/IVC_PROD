using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class EventType : EntityBase
    {
        public int EventTypeID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Name { get; set; }

        public int? SortOrder { get; set; }
        public bool Notificate { get; set; }
        public bool IsPriceChange { get; set; }
        public bool IsDueDateChange { get; set; }

        //navigation
        public ICollection<Event> Events { get; set; }
    }
}
