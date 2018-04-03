using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeoTracker.Models
{
    public class ProjectType : EntityBase
    {
        public int ProjectTypeID { get; set; }

        public int? SortOrder { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string Name { get; set; }

        //navigation
        public ICollection<Project> Projects { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ProjectType))
                return false;

            return ((ProjectType)obj).ProjectTypeID == this.ProjectTypeID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
