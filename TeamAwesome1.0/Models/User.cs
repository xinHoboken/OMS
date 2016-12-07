//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamAwesome1._0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Labors = new HashSet<Labor>();
        }
    
        public string EmpID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Desc { get; set; }
        public int DeptNo { get; set; }
        public string LoginID { get; set; }
        public Nullable<int> UserTypeNo { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Labor> Labors { get; set; }
        public virtual Login Login { get; set; }
        public virtual Permission Permission { get; set; }
    }
}