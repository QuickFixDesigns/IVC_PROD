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
    public class Item : EntityBase
    {
        public int ItemID { get; set; }

        public int ProjectID { get; set; }

        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Code { get; set; }

        public int? SortKey { get; set; }
        public int? SortOrder { get; set; }

        public int? StatusID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Cannot be longer than 255 characters.")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? LatestStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public Project Project { get; set; }
        public Status Status { get; set; }
        public ICollection<Operation> Operations { get; set; }

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
            if (obj == null || !(obj is Item))
                return false;

            return ((Item)obj).ItemID == this.ItemID;
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
