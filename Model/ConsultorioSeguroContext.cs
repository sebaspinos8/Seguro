using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Seguro.Model;

public partial class ConsultorioSeguroContext : DbContext
{
    public ConsultorioSeguroContext()
    {
    }

    public ConsultorioSeguroContext(DbContextOptions<ConsultorioSeguroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asegurado> Asegurados { get; set; }

    public virtual DbSet<Seguros> Seguros { get; set; }

    public virtual DbSet<Seguroasegurado> Seguroasegurados { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asegurado>(entity =>
        {
            entity.HasKey(e => e.IdAsegurado).HasName("PRIMARY");

            entity.ToTable("asegurado");

            entity.Property(e => e.IdAsegurado)
                .ValueGeneratedNever()
                .HasColumnName("idAsegurado");
            entity.Property(e => e.CedulaAsegurado)
                .HasMaxLength(15)
                .HasColumnName("cedulaAsegurado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.NombreAsegurado)
                .HasMaxLength(100)
                .HasColumnName("nombreAsegurado");
            entity.Property(e => e.TelefonoAsegurado).HasColumnName("telefonoAsegurado");
        });

        modelBuilder.Entity<Seguros>(entity =>
        {
            entity.HasKey(e => e.IdSeguro).HasName("PRIMARY");

            entity.ToTable("seguro");

            entity.HasIndex(e => e.CodigoSeguro, "codigoSeguro_UNIQUE").IsUnique();

            entity.Property(e => e.IdSeguro)
                .ValueGeneratedNever()
                .HasColumnName("idSeguro");
            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(45)
                .HasColumnName("codigoSeguro");
            entity.Property(e => e.NombreSeguro)
                .HasMaxLength(100)
                .HasColumnName("nombreSeguro");
            entity.Property(e => e.Prima)
                .HasPrecision(6)
                .HasColumnName("prima");
            entity.Property(e => e.SumaAsegurada)
                .HasPrecision(6)
                .HasColumnName("sumaAsegurada");
        });

        modelBuilder.Entity<Seguroasegurado>(entity =>
        {
            entity.HasKey(e => e.IdseguroAsegurado).HasName("PRIMARY");

            entity.ToTable("seguroasegurado");

            entity.HasIndex(e => e.IdAsegurado, "aseguradoN_idx");

            entity.HasIndex(e => e.IdSeguro, "seguroN_idx");

            entity.Property(e => e.IdseguroAsegurado)
                .ValueGeneratedNever()
                .HasColumnName("idseguroAsegurado");
            entity.Property(e => e.IdAsegurado).HasColumnName("idAsegurado");
            entity.Property(e => e.IdSeguro).HasColumnName("idSeguro");

            entity.HasOne(d => d.IdAseguradoNavigation).WithMany(p => p.Seguroasegurados)
                .HasForeignKey(d => d.IdAsegurado)
                .HasConstraintName("aseguradoN");

            entity.HasOne(d => d.IdSeguroNavigation).WithMany(p => p.Seguroasegurados)
                .HasForeignKey(d => d.IdSeguro)
                .HasConstraintName("seguroN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
