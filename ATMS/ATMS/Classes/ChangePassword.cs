using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMS_TestingSubject.Classes
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,30})$", ErrorMessage = "*Should Be strong and 8 TO 32 Char")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("NewPassword", ErrorMessage = "Not Match")]
        [DataType(DataType.Password)]
        public string ComparePassward { get; set; }


    }
}