using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaAprobado",
                table: "Examenes");

            migrationBuilder.AddColumn<bool>(
                name: "EstaAprobado",
                table: "UsuarioExamen",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaAprobado",
                table: "UsuarioExamen");

            migrationBuilder.AddColumn<bool>(
                name: "EstaAprobado",
                table: "Examenes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
