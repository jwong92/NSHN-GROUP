
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApplication1.Models
{

using System;
    using System.Collections.Generic;
    
public partial class site
{

    public site()
    {

        this.departments = new HashSet<department>();

    }


    public int id { get; set; }

    public string name { get; set; }

    public string phone { get; set; }

    public string address { get; set; }

    public string city { get; set; }

    public string province { get; set; }

    public string postal_code { get; set; }



    public virtual ICollection<department> departments { get; set; }

}

}
