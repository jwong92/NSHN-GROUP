using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [MetadataType(typeof(messagemetadata))]
    public partial class message
    {
        public class messagemetadata
        {
            [Display(Name = "First Name")]
            [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your first name.")]
            public string first_name { get; set; }

            [Display(Name = "Last Name")]
            [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your first name.")]
            public string last_name { get; set; }

            [Display(Name = "Email Address")]
            [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email address")]
            public string email_address { get; set; }

            [Display(Name = "Message")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Don't forget to add a message!")]
            public string message_body { get; set; }
        }
    }
}