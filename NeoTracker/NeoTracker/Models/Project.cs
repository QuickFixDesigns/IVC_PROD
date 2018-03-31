using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class Project : EntityBase
    {
        public int ProjectID { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Code { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string PurchaseOrder { get; set; }

        [StringLength(255, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Client { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Cannot be longer than 255 characters.")]
        public string Comment { get; set; }

        public int? ProjectTypeID { get; set; }

        //navigation
        public ProjectType ProjectType { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
