namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ATMS_Model : DbContext
    {
        public ATMS_Model()
            : base("name=ATMS_Model")
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Leaving> Leavings { get; set; }
        public virtual DbSet<MeetingInfo> MeetingInfoes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<UsersInMeeting> UsersInMeetings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepName)
                .IsUnicode(false);

            modelBuilder.Entity<Leaving>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<Leaving>()
                .Property(e => e.LeaveRequest)
                .IsUnicode(false);

            modelBuilder.Entity<Leaving>()
                .Property(e => e.LeaveState)
                .IsUnicode(false);

            modelBuilder.Entity<MeetingInfo>()
                .HasMany(e => e.UsersInMeetings)
                .WithRequired(e => e.MeetingInfo)
                .HasForeignKey(e => e.MeetingId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Passward)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.MeetingInfoes)
                .WithOptional(e => e.UserInfo)
                .HasForeignKey(e => e.HeadId);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UsersInMeetings)
                .WithRequired(e => e.UserInfo)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
