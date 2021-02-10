using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_API.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "crveniUsername",
                table: "Igra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "plaviUsername",
                table: "Igra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zeleniUsername",
                table: "Igra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zutiUsername",
                table: "Igra",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "crveniUsername",
                table: "Igra");

            migrationBuilder.DropColumn(
                name: "plaviUsername",
                table: "Igra");

            migrationBuilder.DropColumn(
                name: "zeleniUsername",
                table: "Igra");

            migrationBuilder.DropColumn(
                name: "zutiUsername",
                table: "Igra");
        }
    }
}
