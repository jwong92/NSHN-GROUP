
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
    
public partial class province
{

    public province()
    {

        this.north_shore_accounts = new HashSet<north_shore_accounts>();

    }


    public string code { get; set; }

    public string name { get; set; }



    public virtual ICollection<north_shore_accounts> north_shore_accounts { get; set; }

}

}