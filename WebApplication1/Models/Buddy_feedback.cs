using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(feedbackMetaData))]
    public partial class feedback
    {
        public class feedbackMetaData
        {

            public feedbackMetaData() {
                date_submitted = DateTime.Now;
            }

            public int feedback_id { get; set; }

            public int user_id { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "1. Please tell us the date and location of your visit:")]
            public string date_location { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "2. What difficulties did you experience accessing our goods and services?")]
            public string problem { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "3. What suggestions do you have to help us improve accessibility?")]
            public string suggestion { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "4. Name and contact details (optional):")]
            public string detail { get; set; }

            public DateTime date_submitted { get; set; }

        }
    }
}