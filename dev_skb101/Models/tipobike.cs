//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dev_skb101.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class tipobike
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipobike()
        {
            this.aluguel = new HashSet<aluguel>();
        }
    
        public int id { get; set; }
        [DisplayName("Valor por Hora")]
        public Nullable<decimal> valorHora { get; set; }
        [DisplayName("Valor por dia")]
        public Nullable<decimal> valorDiaria { get; set; }
        [DisplayName("Tipo de Aluguel")]
        public string descicao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aluguel> aluguel { get; set; }
    }
}
