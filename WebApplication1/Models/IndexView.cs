using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class IndexView
    {
        public List<image> image { get; set; }
        public List<news> news { get; set; }
    }
}