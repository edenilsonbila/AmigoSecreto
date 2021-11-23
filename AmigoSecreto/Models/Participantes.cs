namespace AmigoSecreto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Participantes
    {
        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public int? AmigoSecretoId { get; set; }

        [StringLength(1)]
        public string Sorteado { get; set; }

        public int? QuemSorteouId { get; set; }

        public int? PessoaTiradaId { get; set; }

        public virtual AmigoSecreto AmigoSecreto { get; set; }

        public virtual Usuario Usuario { get; set; }

        [ForeignKey("QuemSorteouId")]
        public virtual Usuario QuemSorteou { get; set; }
        [ForeignKey("PessoaTiradaId")]
        public virtual Usuario PessoaTirada { get; set; }
    }
}
