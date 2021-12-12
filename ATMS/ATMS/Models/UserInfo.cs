namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public enum GenderOptions
    {
        Male, Female
    }
    public enum TypeOptions
    {
        Admin,
        Head,
        Employee
    }
    [Table("UserInfo")]
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            MeetingInfoes = new HashSet<MeetingInfo>();
            Tickets = new HashSet<Ticket>();
            UsersInMeetings = new HashSet<UsersInMeeting>();
        }
       
        public int Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(25, ErrorMessage = "Max Length is 25 Char")]
        [MinLength(5, ErrorMessage = "Min Length is 5 Char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Passward { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        public int? DepId { get; set; }

        public bool? Active { get; set; }

        public bool? Accepted { get; set; }
        [Display(Name= "AbsenceSeconds")]
        public int? AbsenceHours { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingInfo> MeetingInfoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersInMeeting> UsersInMeetings { get; set; }
    }
}
