using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class User : EntityBase
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Alias { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320, ErrorMessage = "Cannot be longer than 320 characters.")]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public bool EmailNotifications { get; set; }

        public ICollection<DepartmentUser> DepartmentUsers { get; set; }


        public string LongName
        {
            get
            {
                return string.Concat(LastName, ", ", FirstName, " (", Alias, ")");
            }
        }
        public UserViewModel ToViewModel()
        {
            return new UserViewModel()
            {
                UserID = UserID,
                Alias = Alias,
                CreatedAt = CreatedAt,
                Email = Email,
                EmailNotifications = EmailNotifications,
                FirstName = FirstName,
                LastName = LastName,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
                return false;

            return ((User)obj).UserID == this.UserID;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
