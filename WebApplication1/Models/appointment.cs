
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
    
public partial class appointment
{

    public int appointment_id { get; set; }

    public string full_name { get; set; }

    public int phone_number { get; set; }

    public System.DateTime appointment_date { get; set; }

    public string detail { get; set; }

    public Nullable<int> user_id { get; set; }

}

}
