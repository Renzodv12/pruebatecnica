using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Context;

public partial class PruebaTecnicaDbContext : DbContext
{
    public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Cuotum> Cuota { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.ToTable("Contrato");

            entity.Property(e => e.Cliente).HasMaxLength(100);
        });

        modelBuilder.Entity<Cuotum>(entity =>
        {
            entity.HasKey(e => e.CuotaId);

            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.ContratoId)
                .HasConstraintName("FK_Cuota_Contrato");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
