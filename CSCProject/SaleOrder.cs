//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSCProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime Date { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
