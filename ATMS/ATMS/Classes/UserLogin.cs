using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMS_TestingSubject.Classes
{
    public class UserLogin
    {
        [Required(ErrorMessage ="*")]
        public string Type { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        public string Passward { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }
    }
}