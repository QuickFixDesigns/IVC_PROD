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
    public class Department : EntityBase
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Name { get; set; }

        public int? SortOrder { get; set; }

        public int? HeadOfDepartmentID { get; set; }

        [ForeignKey("HeadOfDepartmentID")]
        public User HeadOfDepartment { get; set; }

        //navigation
        public ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public ICollection<DepartmentOperation> DepartmentOperations { get; set; }
        public ICollection<Operation> ItemOperations { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Department))
                return false;

            return ((Department)obj).DepartmentID == this.DepartmentID;
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
