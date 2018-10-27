using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Job_ProtalBd.Models
{
    public class Login
    {
        [Display(Name = "E_Mail Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = " E_Mail  Id Required")]
        [DataType(DataType.EmailAddress)]
        public string E_Mail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}