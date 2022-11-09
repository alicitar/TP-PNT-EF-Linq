using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(nullable: true),
                    Contrasenia = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Dni = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    TipoPerfil = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstaAprobado = table.Column<bool>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examenes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enunciado = table.Column<string>(nullable: true),
                    ExamenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_preguntas_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(nullable: true),
                    EsCorrecta = table.Column<bool>(nullable: false),
                    PreguntaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opciones_preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_UsuarioId",
                table: "Examenes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Opciones_PreguntaId",
                table: "Opciones",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_preguntas_ExamenId",
                table: "preguntas",
                column: "ExamenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opciones");

            migrationBuilder.DropTable(
                name: "preguntas");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
