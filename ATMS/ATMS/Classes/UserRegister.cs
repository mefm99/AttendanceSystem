using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMS_TestingSubject.Classes
{
    public class UserRegister
    {
        [Required(ErrorMessage ="Type is Required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(25, ErrorMessage = "Max Length is 25 Char")]
        [MinLength(5, ErrorMessage = "Min Length is 5 Char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Passward { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Passward", ErrorMessage = "Not Match")]
        [DataType(DataType.Password)]
        public string ComparePassward { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Department is Required")]
        public int DepId { get; set; }
    }
}