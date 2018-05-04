using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeoTracker.Models
{
    public class ReportLink : EntityBase
    {
        public int ReportLinkID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "Cannot be longer than 255 characters.")]
        public string Name{ get; set; }

        [Url]
        public string Link { get; set; }
    }
}
