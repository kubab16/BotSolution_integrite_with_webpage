using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Udate_v_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddSeries",
                table: "permisionGlobal",
                newName: "EditSeries");

            migrationBuilder.RenameColumn(
                name: "AddPlayer",
                table: "permisionGlobal",
                newName: "EditPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditSeries",
                table: "permisionGlobal",
                newName: "AddSeries");

            migrationBuilder.RenameColumn(
                name: "EditPlayer",
                table: "permisionGlobal",
                newName: "AddPlayer");
        }
    }
}
