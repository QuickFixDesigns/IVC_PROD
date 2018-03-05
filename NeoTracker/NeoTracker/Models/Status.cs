using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class Status : EntityBase
    {
        public int StatusID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot be longer than 25 characters.")]
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }

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
    }
}
