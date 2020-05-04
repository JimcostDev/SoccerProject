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

    public partial class Entrenador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entrenador()
        {
            this.Equipo = new HashSet<Equipo>();
        }

        public int ent_codigo { get; set; }
        [DisplayName("Nombre")]
        [Required]
        public string ent_nombres { get; set; }
        [DisplayName("Primer Apellido")]
        [Required]
        public string ent_primerApellido { get; set; }
        [DisplayName("Primer Apellido")]
        public string ent_segundoApellido { get; set; }
        [DisplayName("Teléfono")]
        public string ent_telefono { get; set; }
        [DisplayName("Correo eléctronico")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ent_correo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipo> Equipo { get; set; }
    }
}
