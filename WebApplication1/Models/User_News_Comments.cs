using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User_News_Comments
    {
        public news news { get; set; }
        public comment comment { get; set; }
        public user user { get; set; }
    }
}