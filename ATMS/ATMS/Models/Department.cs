namespace ATMS_TestingSubject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            UserInfoes = new HashSet<UserInfo>();
        }

        [Key]
        public int DepId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Required, Please Enter Department Name")]
        [MinLength(2, ErrorMessage = "Should be more than 2 char")]
        [MaxLength(20, ErrorMessage = "Should be less than 2 char")]
        public string DepName { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
