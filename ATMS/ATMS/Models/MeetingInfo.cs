namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeetingInfo")]
    public partial class MeetingInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingInfo()
        {
            UsersInMeetings = new HashSet<UsersInMeeting>();
        }

        public int Id { get; set; }

        public int? HeadId { get; set; }

        public bool? State { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersInMeeting> UsersInMeetings { get; set; }
    }
}
