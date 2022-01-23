using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamingo_Airways.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [MinLength(1)]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "last Name")]
        [MinLength(1)]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "password should be of minimum length 4")]
        [Display(Name = "Password")]
        public string Pwd { get; set; }

        [Required,RegularExpression(@"^([0-9]{10})$")]
        public long PhoneNo { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18,80)]
        public int Age { get; set; }
    }
}