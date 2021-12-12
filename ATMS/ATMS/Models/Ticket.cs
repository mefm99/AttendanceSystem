namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            Attendances = new HashSet<Attendance>();
            Leavings = new HashSet<Leaving>();
        }

        [Key]
        [Display(Name ="barcode")]
        public int RollNo { get; set; }

        public int? Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public TimeSpan? time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leaving> Leavings { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
