using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstitutoIdioma.Models;

namespace InstitutoIdioma.Context
{
    public class InstitutoDatabaseContext : DbContext
    {
        public InstitutoDatabaseContext(DbContextOptions<InstitutoDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Examen> Examenes { get; set; }
        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioExamen> UsuarioExamen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioExamen>()
            .HasKey(ue => new { ue.UsuarioId, ue.ExamenId });

            modelBuilder.Entity<UsuarioExamen>()
            .HasOne(ue => ue.Usuario)
            .WithMany(u => u.Examenes)
            .HasForeignKey(ue => ue.UsuarioId);

            modelBuilder.Entity<UsuarioExamen>()
            .HasOne(ue => ue.Examen)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(ue => ue.ExamenId);

            modelBuilder.Entity<Examen>()
            .HasMany(e => e.Preguntas)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pregunta>()
            .HasOne(p => p.Examen)
            .WithMany(e => e.Preguntas)
            .HasConstraintName("ForeignKey_Preguntas_Examen");

            modelBuilder.Entity<Pregunta>()
            .HasMany(p => p.Opciones)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Opcion>()
            .HasOne(o => o.Pregunta)
            .WithMany(p => p.Opciones)
            .HasConstraintName("ForeignKey_Opciones_Pregunta");
        }
    }
}

