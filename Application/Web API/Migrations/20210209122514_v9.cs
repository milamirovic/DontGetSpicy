using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_API.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StanjeIgre",
                table: "Igra");

            migrationBuilder.AddColumn<int>(
                name: "izabranaFiguraID",
                table: "Potezi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Figure",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    index = table.Column<int>(type: "int", nullable: false),
                    boja = table.Column<int>(type: "int", nullable: false),
                    igraID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figure", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Figure_Igra_igraID",
                        column: x => x.igraID,
                        principalTable: "Igra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Potezi_izabranaFiguraID",
                table: "Potezi",
                column: "izabranaFiguraID");

            migrationBuilder.CreateIndex(
                name: "IX_Figure_igraID",
                table: "Figure",
                column: "igraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Potezi_Figure_izabranaFiguraID",
                table: "Potezi",
                column: "izabranaFiguraID",
                principalTable: "Figure",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Potezi_Figure_izabranaFiguraID",
                table: "Potezi");

            migrationBuilder.DropTable(
                name: "Figure");

            migrationBuilder.DropIndex(
                name: "IX_Potezi_izabranaFiguraID",
                table: "Potezi");

            migrationBuilder.DropColumn(
                name: "izabranaFiguraID",
                table: "Potezi");

            migrationBuilder.AddColumn<string>(
                name: "StanjeIgre",
                table: "Igra",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
