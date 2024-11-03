﻿using Api_TurneroPeluqueria.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_TurneroPeluqueria.Data;

using Microsoft.EntityFrameworkCore;

public class TurneroPeluqueriaContext : DbContext
{
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }
    public DbSet<Pago> Pagos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relación Usuario - Rol (1 a muchos)
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.IdRol);

        // Relación Turno - Usuario (cliente)
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Usuario)
            .WithMany(u => u.TurnosCliente)
            .HasForeignKey(t => t.IdUsuario);

        // Relación Turno - Usuario (peluquero)
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Peluquero)
            .WithMany(u => u.TurnosPeluquero)
            .HasForeignKey(t => t.IdPeluquero);

        // Relación Turno - Servicio
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Servicio)
            .WithMany(s => s.Turnos)
            .HasForeignKey(t => t.IdServicio);

        // Relación HorarioDisponible - Usuario (peluquero)
        modelBuilder.Entity<HorarioDisponible>()
            .HasOne(h => h.Peluquero)
            .WithMany(u => u.HorariosDisponibles)
            .HasForeignKey(h => h.IdPeluquero);

        // Relación Pago - Turno
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Turno)
            .WithMany(t => t.Pagos)
            .HasForeignKey(p => p.IdTurno);
    }
}
