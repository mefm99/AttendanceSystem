namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum States
    {
        Pending, Aprroved, NotApproved
    }
    [Table("Leaving")]
    public partial class Leaving
    {
        public int? RollNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [Key]
        public int LeaveId { get; set; }

        [StringLength(200)]
        [Display(Name = "Reason for leaving")]
        public string LeaveRequest { get; set; }

        [StringLength(50)]
        public string LeaveState { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
