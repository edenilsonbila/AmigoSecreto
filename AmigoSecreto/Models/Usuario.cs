using System.ComponentModel;

namespace AmigoSecreto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public int Id { get; set; }

        [Required(ErrorMessage = "Digite um nome."), Column(Order = 1)]
        [MinLength(2, ErrorMessage = "O tamanho m�nimo do nome s�o 2 caracteres.")]
        [StringLength(50, ErrorMessage = "O tamanho m�ximo s�o 50 caracteres.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um Usu�rio.")]
        [MinLength(2, ErrorMessage = "O tamanho m�nimo do nome s�o 2 caracteres.")]
        [StringLength(50, ErrorMessage = "O tamanho m�ximo s�o 10 caracteres.")]
        [DisplayName("Usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite uma senha."), Column(Order = 2)]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "O tamanho m�nimo da senha s�o 5 caracteres.")]
        public string Senha { get; set; }

        [DisplayName("G�nero")]
        [StringLength(1, ErrorMessage = "O tamanho m�ximo � de 1 caracter.")]

        public string Genero { get; set; }


        public int? Idade { get; set; }

        [DisplayName("Tamanho Cal�ado")]
        public int? TamCalcado { get; set; }

        [DisplayName("Tamanho da Camisa")]
        [StringLength(5)]
        public string TamCamisa { get; set; }

        [DisplayName("Tamanho da Cal�a")]
        [StringLength(5)]
        public string TamCalca { get; set; }

        [DisplayName("Sugest�o Presente")]
        [StringLength(50)]
        public string SugestaoPresente { get; set; }

    }
}
