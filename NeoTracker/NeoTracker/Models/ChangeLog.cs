using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class ChangeLog
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string EntityName { get; set; }

        [StringLength(100)]
        public string PropertyName { get; set; }

        public int PrimaryKeyValue { get; set; }

        [StringLength(255)]
        public string OldValue { get; set; }

        [StringLength(255)]
        public string NewValue { get; set; }

        public DateTime UpdatedAt { get; set; }

        [StringLength(320)]
        public string UpdatedBy { get; set; }
    }
}
