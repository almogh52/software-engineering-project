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
    
    public partial class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int PostalCode { get; set; }
        public int EmployeeTypeId { get; set; }
        public sbyte Deleted { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
    }
}
