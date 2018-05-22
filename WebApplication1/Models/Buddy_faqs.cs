using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [MetadataType(typeof(faqmetadata))]
    public partial class faq
    {
        public class faqmetadata
        {
            [Key]
            public int id { get; set; }

            [Display(Name = "Question")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Question cannot be empty.")]
            public string question { get; set; }

            [Display(Name = "Answer")]
            public string answer { get; set; }

            [Display(Name = "Date Published")]
            public DateTime date_created { get; set; }

            [Display(Name = "Publisher")]
            public int publisher_id { get; set; }

            [Display(Name = "Category")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a category.")]
            public int category_id { get; set; }
        }
    }
}