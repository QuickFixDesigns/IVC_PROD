//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int EventID { get; set; }
        public int ProjectID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public int EventTypeID { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual Item Item { get; set; }
        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
    }
}