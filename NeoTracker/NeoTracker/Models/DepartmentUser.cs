using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.Models
{
    public class DepartmentUser : EntityBase
    {
        public int DepartmentUserID { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }

        public Department Department { get; set; }
        public User User { get; set; }
    }
}
