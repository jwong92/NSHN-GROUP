
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
    
public partial class Tour
{

    public Tour()
    {

        this.TourRequests = new HashSet<TourRequest>();

    }


    public int TourID { get; set; }

    public string TourTime { get; set; }

    public string TourName { get; set; }

    public string GuideName { get; set; }



    public virtual ICollection<TourRequest> TourRequests { get; set; }

}

}
