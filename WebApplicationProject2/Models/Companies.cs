//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationProject2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Companies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Companies()
        {
            this.Complaint = new HashSet<Complaint>();
        }
    
        public int CompanyIDNumber { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CompanyEstablishmentDate { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyType { get; set; }
        public string CompanyEmail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaint { get; set; }
    }
}