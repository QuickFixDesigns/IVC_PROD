//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataManagement.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepartmentUser
    {
        public int DepartmentUserID { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
    }
}
