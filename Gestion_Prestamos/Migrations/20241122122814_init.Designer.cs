﻿// <auto-generated />
using System;
using Gestion_Prestamos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gestion_Prestamos.Migrations
{
    [DbContext(typeof(GestionPrestamosContext))]
    [Migration("20241122122814_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestion_Prestamos.Models.CarritoDePrestamo", b =>
                {
                    b.Property<int>("CarritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarritoId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("ElementoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAgregado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("CarritoId");

                    b.HasIndex("ElementoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("CarritoDePrestamos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoriaId")
                        .HasName("PK__Categori__F353C1E5C2C11CA5");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartamentoId"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DepartamentoId")
                        .HasName("PK__Departam__66BB0E3EEA163775");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Elemento", b =>
                {
                    b.Property<int>("ElementoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElementoId"));

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool?>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("FechaAdquisicion")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ElementoId")
                        .HasName("PK__Elemento__5F6F78ED733555B0");

                    b.HasIndex(new[] { "CategoriaId" }, "IX_Elementos_CategoriaId");

                    b.ToTable("Elementos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Inventario", b =>
                {
                    b.Property<int>("InventarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventarioId"));

                    b.Property<int?>("ElementoId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadesDisponibles")
                        .HasColumnType("int");

                    b.Property<int>("UnidadesTotales")
                        .HasColumnType("int");

                    b.HasKey("InventarioId")
                        .HasName("PK__Inventar__FB8A24D7A470EE39");

                    b.HasIndex(new[] { "ElementoId" }, "IX_Inventarios_ElementoId");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Prestamo", b =>
                {
                    b.Property<int>("PrestamoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrestamoId"));

                    b.Property<int?>("ElementoId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("FechaDevolucion")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaDevolucionCreadoEn")
                        .HasColumnType("datetime");

                    b.Property<string>("FechaDevolucionCreadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaDevolucionModificadoEn")
                        .HasColumnType("datetime");

                    b.Property<string>("FechaDevolucionModificadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateOnly?>("FechaEntrega")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaEntregaCreadoEn")
                        .HasColumnType("datetime");

                    b.Property<string>("FechaEntregaCreadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaEntregaModificadoEn")
                        .HasColumnType("datetime");

                    b.Property<string>("FechaEntregaModificadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaPrestamo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaPrestamoCreadoEn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FechaPrestamoCreadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaPrestamoModificadoEn")
                        .HasColumnType("datetime");

                    b.Property<string>("FechaPrestamoModificadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("UnidadesPrestadas")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("PrestamoId")
                        .HasName("PK__Prestamo__AA58A0A0D7146178");

                    b.HasIndex(new[] { "ElementoId" }, "IX_Prestamos_ElementoId");

                    b.HasIndex(new[] { "UsuarioId" }, "IX_Prestamos_UsuarioId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Role", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolId"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RolId")
                        .HasName("PK__Roles__F92302F1971C8BBF");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UsuarioId")
                        .HasName("PK__Usuarios__2B3DE7B81D617E65");

                    b.HasIndex(new[] { "DepartamentoId" }, "IX_Usuarios_DepartamentoId");

                    b.HasIndex(new[] { "Email" }, "UQ__Usuarios__A9D10534E032D38A")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.UsuarioRol", b =>
                {
                    b.Property<int>("UsuarioRolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioRolId"));

                    b.Property<DateTime?>("FechaAsignacion")
                        .HasColumnType("datetime");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioRolId");

                    b.HasIndex(new[] { "RolId" }, "IX_UsuarioRols_RolId");

                    b.HasIndex(new[] { "UsuarioId" }, "IX_UsuarioRols_UsuarioId");

                    b.ToTable("UsuarioRols");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.CarritoDePrestamo", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Elemento", "Elemento")
                        .WithMany("CarritoDePrestamos")
                        .HasForeignKey("ElementoId")
                        .IsRequired();

                    b.HasOne("Gestion_Prestamos.Models.Usuario", "Usuario")
                        .WithMany("CarritoDePrestamos")
                        .HasForeignKey("UsuarioId")
                        .IsRequired();

                    b.Navigation("Elemento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Elemento", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Categoria", "Categoria")
                        .WithMany("Elementos")
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("FK_Elementos_Categorias");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Inventario", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Elemento", "Elemento")
                        .WithMany("Inventarios")
                        .HasForeignKey("ElementoId")
                        .HasConstraintName("FK_Inventarios_Elementos");

                    b.Navigation("Elemento");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Prestamo", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Elemento", "Elemento")
                        .WithMany("Prestamos")
                        .HasForeignKey("ElementoId")
                        .HasConstraintName("FK_Prestamos_Elementos");

                    b.HasOne("Gestion_Prestamos.Models.Usuario", "Usuario")
                        .WithMany("Prestamos")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_Prestamos_Usuarios");

                    b.Navigation("Elemento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Usuario", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Departamento", "Departamento")
                        .WithMany("Usuarios")
                        .HasForeignKey("DepartamentoId")
                        .HasConstraintName("FK_Usuarios_Departamentos");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.UsuarioRol", b =>
                {
                    b.HasOne("Gestion_Prestamos.Models.Role", "Rol")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UsuarioRol_Roles");

                    b.HasOne("Gestion_Prestamos.Models.Usuario", "Usuario")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UsuarioRol_Usuarios");

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Categoria", b =>
                {
                    b.Navigation("Elementos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Departamento", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Elemento", b =>
                {
                    b.Navigation("CarritoDePrestamos");

                    b.Navigation("Inventarios");

                    b.Navigation("Prestamos");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Role", b =>
                {
                    b.Navigation("UsuarioRols");
                });

            modelBuilder.Entity("Gestion_Prestamos.Models.Usuario", b =>
                {
                    b.Navigation("CarritoDePrestamos");

                    b.Navigation("Prestamos");

                    b.Navigation("UsuarioRols");
                });
#pragma warning restore 612, 618
        }
    }
}