using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(appointmentMetaData))]
    public partial class appointment
    {
        public class appointmentMetaData
        {

            public int appointment_id { get; set; }

            public int user_id { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "1. Please Enter Your Full Name:")]
            public string full_name { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "2. Please Enter Your Phone Number:")]
            public int phone_number { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "3. Please choose your preferred appointment date:")]
            public DateTime appointment_date { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the required information")]
            [Display(Name = "4. Please give us some details about your visit:")]
            public string detail { get; set; }

        }
    }
}