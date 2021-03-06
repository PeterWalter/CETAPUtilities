//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CETAPUtilities.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.RegisteredEmployees = new HashSet<RegisteredEmployee>();
        }
    
        public int ID { get; set; }
        public int IDType { get; set; }
        public string ID_Passport { get; set; }
        public int TitleID { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public int GenderID { get; set; }
        public System.DateTime DOB { get; set; }
        public int NationalityID { get; set; }
        public int ClassID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public Nullable<System.DateTime> FirstPaymentDate { get; set; }
        public bool CurrentEmployee { get; set; }
        public System.Guid RowGuid { get; set; }
        public System.DateTime DateModified { get; set; }
    
        public virtual Classification Classification { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual IDType IDType1 { get; set; }
        public virtual Title Title { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegisteredEmployee> RegisteredEmployees { get; set; }
    }
}
