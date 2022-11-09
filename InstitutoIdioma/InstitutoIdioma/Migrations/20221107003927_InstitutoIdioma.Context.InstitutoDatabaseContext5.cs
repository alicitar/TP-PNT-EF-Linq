using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examenes_Usuarios_UsuarioId",
                table: "Examenes");

            migrationBuilder.DropIndex(
                name: "IX_Examenes_UsuarioId",
                table: "Examenes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Examenes");

            migrationBuilder.CreateTable(
                name: "UsuarioExamen",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    ExamenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioExamen", x => new { x.UsuarioId, x.ExamenId });
                    table.ForeignKey(
                        name: "FK_UsuarioExamen_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioExamen_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioExamen_ExamenId",
                table: "UsuarioExamen",
                column: "ExamenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioExamen");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Examenes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_UsuarioId",
                table: "Examenes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examenes_Usuarios_UsuarioId",
                table: "Examenes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
