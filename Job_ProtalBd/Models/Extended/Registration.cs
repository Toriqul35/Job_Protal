using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Job_ProtalBd.Models
{
    [MetadataType(typeof(RegistrationMetadata))]
    public partial class Registration
    {
        public string ConfrimPassword { get; set; }
    }
    public class RegistrationMetadata
    {
        [Display(Name = "E_Mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = " E_Mail Required")]
        [DataType(DataType.EmailAddress)]
        public string E_Mail { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First namerequired")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "State")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The state required")]
        public string State { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The City required")]
        public string City { get; set; }

        [Display(Name = "Gender")]
       [Required(AllowEmptyStrings = false, ErrorMessage = "Gender required")]
        public string Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/mm/yyy}")]
        public DateTime Date_Of_Birth { get; set; }

        [Display(Name = "Date Of Entry")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/mm/yyy}")]
        public DateTime Date_Of_Entry { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " Password Required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Minimum 4 characters number")]
        public string Password { get; set; }

        [Display(Name = "Confrim Pasword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Do not match the password")]
        public string ConfrimPassword { get; set; }
    }
}