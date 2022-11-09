using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Usuarios");
        }
    }
}
