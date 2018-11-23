namespace TaskSolution.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Task_BD : DbContext
    {
        public Task_BD()
            : base("name=Task_BD")
        {
        }

        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<tarefa> tarefa { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categoria>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .Property(e => e.cancelado)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .HasMany(e => e.tarefa)
                .WithRequired(e => e.categoria)
                .HasForeignKey(e => e.categoria_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tarefa>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<tarefa>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<tarefa>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tarefa>()
                .Property(e => e.cancelado)
                .IsUnicode(false);

            modelBuilder.Entity<tarefa>()
                .HasMany(e => e.tarefa1)
                .WithOptional(e => e.tarefa2)
                .HasForeignKey(e => e.tarefa_id);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nome_completo)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.senha)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.cancelado)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.tarefa)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
