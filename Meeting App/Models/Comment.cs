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
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public string project_Name { get; set; }
        public string Comment1 { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
        public string CommentTime { get; set; }
        public Nullable<int> Status { get; set; }
        public string HostUser { get; set; }
        public Nullable<int> MeetingID { get; set; }
        public Nullable<int> ActionID { get; set; }
        public Nullable<int> DecisionID { get; set; }
    
        public virtual Action_Item Action_Item { get; set; }
        public virtual Decision_Item Decision_Item { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
