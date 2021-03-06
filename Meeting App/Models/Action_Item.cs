//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Meeting_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Action_Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Action_Item()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int ActionItemID { get; set; }
        public string ActionItem_Title { get; set; }
        public string Action_Description { get; set; }
        public string project_Name { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string ActionTime { get; set; }
        public string ActionAssignedTo { get; set; }
        public Nullable<int> Status { get; set; }
        public string Priority { get; set; }
        public Nullable<int> MeetingID { get; set; }
    
        public virtual Meeting Meeting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
