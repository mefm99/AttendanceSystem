namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public int? RollNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [Key]
        public int AttendId { get; set; }

        public bool? State { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
