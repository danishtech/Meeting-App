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
    
    public partial class Decision_Item
    {
        public int DecisionItemID { get; set; }
        public string DecisionItem_Title { get; set; }
        public string project_Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public string DecisionTime { get; set; }
        public string DecisionAssignedTo { get; set; }
        public string Priority { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> MeetingID { get; set; }
    
        public virtual Meeting Meeting { get; set; }
    }
}
