using Api_TurneroPeluqueria.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_TurneroPeluqueria.Data;

public class TurneroDbContext : DbContext
{
    public TurneroDbContext(DbContextOptions<TurneroDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Turno> Turnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de relaciones
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Cliente)
            .WithMany(c => c.Turnos)
            .HasForeignKey(t => t.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Servicio)
            .WithMany(s => s.Turnos)
            .HasForeignKey(t => t.ServicioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Empleado)
            .WithMany(e => e.Turnos)
            .HasForeignKey(t => t.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Restricciones y propiedades adicionales si son necesarias
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Email)
            .IsRequired();

        modelBuilder.Entity<Empleado>()
            .Property(e => e.Email)
            .IsRequired();

        modelBuilder.Entity<Servicio>()
            .Property(s => s.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Turno>()
            .Property(t => t.Estado)
            .HasMaxLength(50)
            .IsRequired();
    }
}
