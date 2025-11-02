using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodaInvitacionApp.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Confirmacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroDePersonas = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeroDeMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    Mensaje = table.Column<string>(type: "TEXT", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    EnvioInvitacion = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConfirmoAsistencia = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConfirmoNoPases = table.Column<int>(type: "INTEGER", nullable: false),
                    PasesDisponibles = table.Column<int>(type: "INTEGER", nullable: false),
                    LinkWhatsApp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirmacion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confirmacion");
        }
    }
}
