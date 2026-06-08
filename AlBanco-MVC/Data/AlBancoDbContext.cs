using AlBanco_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlBanco_MVC.Data
{
    public class AlBancoDbContext : IdentityDbContext

    {
        public AlBancoDbContext(DbContextOptions<AlBancoDbContext> options) : base(options)
        {

        }
        public DbSet<Zona> Zonas => Set<Zona>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Cancha> Canchas => Set<Cancha>();
        public DbSet<Convocatoria> Convocatorias => Set<Convocatoria>();
        public DbSet<Confirmacion> Confirmaciones => Set<Confirmacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Zona
            modelBuilder.Entity<Zona>()
                .HasIndex(z => z.Nombre)
                .IsUnique()
                .HasDatabaseName("IX_Zona_Nombre");

            // Cancha → Zona
            modelBuilder.Entity<Cancha>()
                .HasOne(c => c.Zona)
                .WithMany(z => z.Canchas)
                .HasForeignKey(c => c.ZonaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario → Cancha
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cancha)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(u => u.CanchaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Convocatoria → Cancha
            modelBuilder.Entity<Convocatoria>()
                .HasOne(c => c.Cancha)
                .WithMany(c => c.Convocatorias)
                .HasForeignKey(c => c.CanchaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Convocatoria → Usuario (creador)
            modelBuilder.Entity<Convocatoria>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.ConvocatoriasCreadas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Confirmacion → Usuario
            modelBuilder.Entity<Confirmacion>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Confirmaciones)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Confirmacion → Convocatoria
            modelBuilder.Entity<Confirmacion>()
                .HasOne(c => c.Convocatoria)
                .WithMany(c => c.Confirmaciones)
                .HasForeignKey(c => c.ConvocatoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice único: un usuario no confirma dos veces la misma convocatoria
            modelBuilder.Entity<Confirmacion>()
                .HasIndex(c => new { c.UsuarioId, c.ConvocatoriaId })
                .IsUnique()
                .HasDatabaseName("IX_Confirmacion_Usuario_Convocatoria");
        }
    }
}
