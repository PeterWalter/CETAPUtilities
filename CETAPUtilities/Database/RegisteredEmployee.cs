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
    
    public partial class RegisteredEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegisteredEmployee()
        {
            this.NBTStaffPayments = new HashSet<NBTStaffPayment>();
        }
    
        public int StaffID { get; set; }
        public long TaxRefNo { get; set; }
        public int VenueCode { get; set; }
        public int IntakeYrID { get; set; }
        public int EmployeeID { get; set; }
        public System.Guid RowGuid { get; set; }
        public System.DateTime DateModified { get; set; }
    
        public virtual IntakeYear IntakeYear { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NBTStaffPayment> NBTStaffPayments { get; set; }
    }
}
