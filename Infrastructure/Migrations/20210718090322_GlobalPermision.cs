using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class GlobalPermision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "raiting",
                table: "Rating",
                newName: "Raiting");

            migrationBuilder.AddColumn<ulong>(
                name: "PermisionGlobalId",
                table: "users",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Characters",
                table: "Rating",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Graphics",
                table: "Rating",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Music",
                table: "Rating",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Story",
                table: "Rating",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "permisionGlobal",
                columns: table => new
                {
                    PermisionGlobalId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Modelator = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddSeries = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddPlayer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Coments = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisionGlobal", x => x.PermisionGlobalId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_users_PermisionGlobalId",
                table: "users",
                column: "PermisionGlobalId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_permisionGlobal_PermisionGlobalId",
                table: "users",
                column: "PermisionGlobalId",
                principalTable: "permisionGlobal",
                principalColumn: "PermisionGlobalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_permisionGlobal_PermisionGlobalId",
                table: "users");

            migrationBuilder.DropTable(
                name: "permisionGlobal");

            migrationBuilder.DropIndex(
                name: "IX_users_PermisionGlobalId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PermisionGlobalId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Characters",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Graphics",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Story",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "Raiting",
                table: "Rating",
                newName: "raiting");
        }
    }
}
