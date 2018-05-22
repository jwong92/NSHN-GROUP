using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(TourGuideMetaData))]
    public partial class TourGuide
    {
        public class TourGuideMetaData
        {
            [Display(Name = "Enter Guide Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter guide name")]
            public string GuideName { get; set; }
            [Display(Name = "Enter Guide Specialty")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter guide specialty")]
            public string GuideSpeciality { get; set; }
            [Display(Name = "Enter Guide Suitability")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter guide suitability")]
            public string GuideSuitability { get; set; }

        }
    }
}