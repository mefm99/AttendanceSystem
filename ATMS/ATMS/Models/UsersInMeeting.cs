namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersInMeeting")]
    public partial class UsersInMeeting
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MeetingId { get; set; }

        public virtual MeetingInfo MeetingInfo { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
