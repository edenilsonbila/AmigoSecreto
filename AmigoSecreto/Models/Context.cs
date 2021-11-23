namespace AmigoSecreto.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=AmigoSecreto")
        {
        }

        public virtual DbSet<AmigoSecreto> AmigoSecreto { get; set; }
        public virtual DbSet<Participantes> Participantes { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participantes>()
                .Property(e => e.Sorteado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Genero)
                .IsFixedLength()
                .IsUnicode(false);

            //modelBuilder.Entity<Usuario>()
            //    .HasMany(e => e.)
            //    .WithOptional(e => e.UsuarioPessoaTirada)
            //    .HasForeignKey(e => e.PessoaTirada);

            //modelBuilder.Entity<Usuario>()
            //    .HasMany(e => e.Participantes1)
            //    .WithOptional(e => e.UsuarioQuemSorteou)
            //    .HasForeignKey(e => e.QuemSorteou);

            //modelBuilder.Entity<Usuario>()
            //    .HasMany(e => e.Participantes2)
            //    .WithOptional(e => e.Usuario)
            //    .HasForeignKey(e => e.UsuarioId);
        }
    }
}
