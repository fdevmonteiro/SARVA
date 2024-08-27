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
    using System.ComponentModel.DataAnnotations;

    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.Item_Venda = new HashSet<Item_Venda>();
        }
    
        public int id_ciclo { get; set; }

        [Required(ErrorMessage = "Esse campo � obrigat�rio")]
        public int codigo { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public Nullable<int> id_empresa { get; set; }

        [Required(ErrorMessage = "Esse campo � obrigat�rio")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Esse campo � obrigat�rio")]
        public Nullable<decimal> valor { get; set; }
        public Nullable<int> pontos { get; set; }
        public Nullable<bool> flag { get; set; }
    
        public virtual Ciclo Ciclo { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item_Venda> Item_Venda { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
