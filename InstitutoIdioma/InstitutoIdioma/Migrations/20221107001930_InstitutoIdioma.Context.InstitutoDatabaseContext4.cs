using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opciones_preguntas_PreguntaId",
                table: "Opciones");

            migrationBuilder.DropForeignKey(
                name: "FK_preguntas_Examenes_ExamenId",
                table: "preguntas");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Opciones_Pregunta",
                table: "Opciones",
                column: "PreguntaId",
                principalTable: "preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Preguntas_Examen",
                table: "preguntas",
                column: "ExamenId",
                principalTable: "Examenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Opciones_Pregunta",
                table: "Opciones");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Preguntas_Examen",
                table: "preguntas");

            migrationBuilder.AddForeignKey(
                name: "FK_Opciones_preguntas_PreguntaId",
                table: "Opciones",
                column: "PreguntaId",
                principalTable: "preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_preguntas_Examenes_ExamenId",
                table: "preguntas",
                column: "ExamenId",
                principalTable: "Examenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
