using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Cannot be longer than 320 characters.")]
        public string CreatedBy { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Cannot be longer than 320 characters.")]
        public string UpdatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
