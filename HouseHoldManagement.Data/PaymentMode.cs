//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HouseHoldManagement.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentMode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentMode()
        {
            this.SpentAmounts = new HashSet<SpentAmount>();
        }
    
        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
        public string PaymentModeDescription { get; set; }
        public Nullable<bool> IsInUse { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpentAmount> SpentAmounts { get; set; }
    }
}