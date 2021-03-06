//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectSoccerPoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Torneo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Torneo()
        {
            this.Partido = new HashSet<Partido>();
        }

        public int tor_codigo { get; set; }
        [DisplayName("Fecha inicial")]
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> tor_fechaInicio { get; set; }
        [DisplayName("Fecha final")]
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> tor_fechaFinal { get; set; }
        [DisplayName("Nombre de torneo")]
        [Required]
        public string tor_nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partido> Partido { get; set; }
    }
}
