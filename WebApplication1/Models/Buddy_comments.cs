using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [MetadataType(typeof(commentMetaData))]
    public partial class comment
    {
        public class commentMetaData
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a comment")]
            [Display(Name = "Comment")]
            public string comment1 { get; set; }
        }
    }
}