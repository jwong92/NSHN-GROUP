
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
    
public partial class category
{

    public category()
    {

        this.faqs = new HashSet<faq>();

    }


    public int id { get; set; }

    public string name { get; set; }



    public virtual ICollection<faq> faqs { get; set; }

}

}
