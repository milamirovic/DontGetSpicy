using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_API.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojPobeda = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Igra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanjeIgre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    kreatorIgreID = table.Column<int>(type: "int", nullable: true),
                    crveniIgracId = table.Column<int>(type: "int", nullable: false),
                    plaviIgracId = table.Column<int>(type: "int", nullable: false),
                    zeleniIgracId = table.Column<int>(type: "int", nullable: false),
                    zutiIgracId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igra_Korisnik_kreatorIgreID",
                        column: x => x.kreatorIgreID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igra_kreatorIgreID",
                table: "Igra",
                column: "kreatorIgreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igra");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
