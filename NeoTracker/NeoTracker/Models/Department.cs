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
        public string Msg { get; set; }
        public bool IsDefault { get; set; }

        [ForeignKey("HeadOfDepartmentID")]
        public User HeadOfDepartment { get; set; }

        public ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public ICollection<ItemOperation> ItemOperations { get; set; }

        //public UserViewModel ToViewModel()
        //{
        //    return new UserViewModel()
        //    {
        //        UserID = UserID,
        //        Alias = Alias,
        //        CreatedAt = CreatedAt,
        //        Email = Email,
        //        EmailNotifications = EmailNotifications,
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        UpdatedAt = UpdatedAt,
        //        UpdatedBy = UpdatedBy
        //    };
        //}
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Department))
                return false;

            return ((Department)obj).DepartmentID == this.DepartmentID;
        }
    }
}
