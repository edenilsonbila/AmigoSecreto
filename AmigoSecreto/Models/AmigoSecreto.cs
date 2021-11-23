namespace AmigoSecreto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmigoSecreto")]
    public partial class AmigoSecreto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AmigoSecreto()
        {
            Participantes = new HashSet<Participantes>();
            SorteioLiberado = "N";
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string Descricao { get; set; }

        public string SorteioLiberado { get; set; }

        public DateTime? DataHora { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Participantes> Participantes { get; set; }
    }
}
