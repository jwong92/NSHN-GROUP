
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
    
public partial class message
{

    public message()
    {

        this.messages1 = new HashSet<message>();

    }


    public int Id { get; set; }

    public string first_name { get; set; }

    public string last_name { get; set; }

    public string email_address { get; set; }

    public string message_body { get; set; }

    public System.DateTime sent_date { get; set; }

    public Nullable<int> reply_id { get; set; }



    public virtual ICollection<message> messages1 { get; set; }

    public virtual message message1 { get; set; }

}

}