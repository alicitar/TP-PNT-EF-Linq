using Microsoft.EntityFrameworkCore.Migrations;

namespace InstitutoIdioma.Migrations
{
    public partial class InstitutoIdiomaContextInstitutoDatabaseContext3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Examenes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Examenes");
        }
    }
}
