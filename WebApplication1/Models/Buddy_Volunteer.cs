using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(VolunteerMetaData))]
    public partial class Volunteer
    {
        public class VolunteerMetaData
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int VolunteerID { get; set; }
            [Display(Name = "First Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your last name with no special characters")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your first name with no special characters")]
            public string LastName { get; set; }
            [Display(Name = "Date of Birth")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Date of Birth")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime Dob { get; set; }
            [Display(Name = "Phone Number")]
            [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Please enter your phone number")]
            public string Phone { get; set; }
            [Display(Name = "Street Address")]
            [RegularExpression(@"\d{1,3}.?\d{0,3}\s[a-zA-Z]{2,30}\s[a-zA-Z]{2,15}", ErrorMessage = "Please enter your phone number")]
            public string StAddress { get; set; }
            [Display(Name = "Availability Date")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your availability date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime dateavailable { get; set; }
            public string username { get; set; }
            [Display(Name = "Email")]
            [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Please enter your phone number")]
            public string Email { get; set; }

        }
    }
}