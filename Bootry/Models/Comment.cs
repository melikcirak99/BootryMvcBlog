//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bootry.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int ID { get; set; }
        public string commentContent { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public bool isApproved { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
