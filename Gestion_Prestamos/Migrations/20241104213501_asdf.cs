using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Prestamos.Migrations
{
    /// <inheritdoc />
    public partial class asdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__F353C1E5C2C11CA5", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departam__66BB0E3EEA163775", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__F92302F1971C8BBF", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Elementos",
                columns: table => new
                {
                    ElementoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaAdquisicion = table.Column<DateOnly>(type: "date", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Elemento__5F6F78ED733555B0", x => x.ElementoId);
                    table.ForeignKey(
                        name: "FK_Elementos_Categorias",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__2B3DE7B81D617E65", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Departamentos",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    InventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementoId = table.Column<int>(type: "int", nullable: true),
                    UnidadesTotales = table.Column<int>(type: "int", nullable: false),
                    UnidadesDisponibles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__FB8A24D7A470EE39", x => x.InventarioId);
                    table.ForeignKey(
                        name: "FK_Inventarios_Elementos",
                        column: x => x.ElementoId,
                        principalTable: "Elementos",
                        principalColumn: "ElementoId");
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    PrestamoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    UnidadesPrestadas = table.Column<int>(type: "int", nullable: false),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDevolucion = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaEntrega = table.Column<DateOnly>(type: "date", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaPrestamoCreadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaPrestamoCreadoEn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaPrestamoModificadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaPrestamoModificadoEn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaDevolucionCreadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaDevolucionCreadoEn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaDevolucionModificadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaDevolucionModificadoEn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaEntregaCreadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaEntregaCreadoEn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaEntregaModificadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaEntregaModificadoEn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prestamo__AA58A0A0D7146178", x => x.PrestamoId);
                    table.ForeignKey(
                        name: "FK_Prestamos_Elementos",
                        column: x => x.ElementoId,
                        principalTable: "Elementos",
                        principalColumn: "ElementoId");
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRols",
                columns: table => new
                {
                    UsuarioRolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRols", x => x.UsuarioRolId);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Roles",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuarios",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elementos_CategoriaId",
                table: "Elementos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ElementoId",
                table: "Inventarios",
                column: "ElementoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_ElementoId",
                table: "Prestamos",
                column: "ElementoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRols_RolId",
                table: "UsuarioRols",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRols_UsuarioId",
                table: "UsuarioRols",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__A9D10534E032D38A",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "UsuarioRols");

            migrationBuilder.DropTable(
                name: "Elementos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
