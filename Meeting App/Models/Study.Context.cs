﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Virtual_StudyEntities : DbContext
    {
        public Virtual_StudyEntities()
            : base("name=Virtual_StudyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Action_Item> Action_Items { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Decision_Item> Decision_Items { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Meeting_Note> Meeting_Notes { get; set; }
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<PollOption> PollOptions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
    }
}
