//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SARVA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ciclo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ciclo()
        {
            this.Produto = new HashSet<Produto>();
        }
    
        public int id { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public string nome { get; set; }
        public Nullable<System.DateTime> dataInicio { get; set; }
        public Nullable<System.DateTime> dataFim { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
