using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Prestamos.Models;

public partial class GestionPrestamosContext : DbContext
{
    public GestionPrestamosContext()
    {
    }

    public GestionPrestamosContext(DbContextOptions<GestionPrestamosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Elemento> Elementos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TOTOMHW\\TOTOMHW;Database=Gestion_Prestamos;User Id=sa;Password=12345;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E5C2C11CA5");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId).HasName("PK__Departam__66BB0E3EEA163775");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Elemento>(entity =>
        {
            entity.HasKey(e => e.ElementoId).HasName("PK__Elemento__5F6F78ED733555B0");

            entity.HasIndex(e => e.CategoriaId, "IX_Elementos_CategoriaId");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Elementos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Elementos_Categorias");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24D7A470EE39");

            entity.HasIndex(e => e.ElementoId, "IX_Inventarios_ElementoId");

            entity.HasOne(d => d.Elemento).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ElementoId)
                .HasConstraintName("FK_Inventarios_Elementos");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.PrestamoId).HasName("PK__Prestamo__AA58A0A0D7146178");

            entity.HasIndex(e => e.ElementoId, "IX_Prestamos_ElementoId");

            entity.HasIndex(e => e.UsuarioId, "IX_Prestamos_UsuarioId");

            entity.Property(e => e.FechaDevolucionCreadoEn).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucionCreadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaDevolucionModificadoEn).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucionModificadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaEntregaCreadoEn).HasColumnType("datetime");
            entity.Property(e => e.FechaEntregaCreadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaEntregaModificadoEn).HasColumnType("datetime");
            entity.Property(e => e.FechaEntregaModificadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaPrestamoCreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPrestamoCreadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaPrestamoModificadoEn).HasColumnType("datetime");
            entity.Property(e => e.FechaPrestamoModificadoPor).HasMaxLength(100);
            entity.Property(e => e.Observaciones).HasMaxLength(250);

            entity.HasOne(d => d.Elemento).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.ElementoId)
                .HasConstraintName("FK_Prestamos_Elementos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Prestamos_Usuarios");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F1971C8BBF");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B81D617E65");

            entity.HasIndex(e => e.DepartamentoId, "IX_Usuarios_DepartamentoId");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534E032D38A").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);

            entity.HasOne(d => d.Departamento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DepartamentoId)
                .HasConstraintName("FK_Usuarios_Departamentos");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasIndex(e => e.RolId, "IX_UsuarioRols_RolId");

            entity.HasIndex(e => e.UsuarioId, "IX_UsuarioRols_UsuarioId");

            entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_UsuarioRol_Roles");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_UsuarioRol_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
