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

    public partial class NBTStaffPosition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NBTStaffPosition()
        {
            this.NBTStaffPayments = new HashSet<NBTStaffPayment>();
        }

        public int PostID { get; set; }
        public string Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NBTStaffPayment> NBTStaffPayments { get; set; }
    }
}
