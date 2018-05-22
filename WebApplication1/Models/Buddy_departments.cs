using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [MetadataType(typeof(donationmetadata))]
    public partial class department
    {
        public class donationmetadata
        {
            [Key]
            [Display(Name = "Department Id")]
            public int dept_id { get; set; }

            [Display(Name = "Department Name")]
            [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "A department name is required")]
            public string dept_name { get; set; }

            [Display(Name = "Description")]
            [MaxLength(250, ErrorMessage = "Please shorten the description to 250 characters.")]
            public string dept_description { get; set; }

            [Display(Name = "Extension")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "An extension number is required")]
            [MaxLength(5, ErrorMessage = "Extension Number cannot exceed 5 digits")]
            [MinLength(3, ErrorMessage = "Extension Number cannot be shorter than 3 digits")]
            public string dept_extension { get; set; }

            [Display(Name = "Date Created")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> date_added { get; set; }

            [Display(Name = "Site Location")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please indicate the corresponding hospital site by choosing from dropdown menu")]
            public int site_id { get; set; }


        }

    }
}