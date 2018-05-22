using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [MetadataType(typeof(donationmetadata))]
    public partial class site
    {
        public class donationmetadata
        {
            [Key]
            public int id { get; set; }

            [Display(Name = "Site Name")]
            [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Site name is required")]
            public string name { get; set; }

            [Display(Name = "Phone")]
            [DataType(DataType.PhoneNumber, ErrorMessage = "Not a valid phone number")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "A phone number is required")]
            public string phone { get; set; }

            [Display(Name = "Address")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "An address is required")]
            public string address { get; set; }

            [Display(Name = "City")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "A city name is required")]
            public string city { get; set; }

            [Display(Name = "Province")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "A province name is required")]
            public string province { get; set; }

            [Display(Name = "Postal Code")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "A postal code is required")]
            [MaxLength(6, ErrorMessage = "Invalid postal code, cannot be more than 6 digits long")]
            [MinLength(6, ErrorMessage = "Postal code must be 6 digits long")]
            [DataType(DataType.PostalCode, ErrorMessage = "Please enter a correct Postal Code")]
            public string postal_code { get; set; }
        }
    }
}