using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_API.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "aleaIactaEst",
                table: "Igra",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "naPotezu",
                table: "Igra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Potezi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    igraID = table.Column<int>(type: "int", nullable: true),
                    potezOdigrao = table.Column<int>(type: "int", nullable: false),
                    vrKocke = table.Column<int>(type: "int", nullable: false),
                    vremeOdigravanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potezi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Potezi_Igra_igraID",
                        column: x => x.igraID,
                        principalTable: "Igra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Potezi_igraID",
                table: "Potezi",
                column: "igraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Potezi");

            migrationBuilder.DropColumn(
                name: "aleaIactaEst",
                table: "Igra");

            migrationBuilder.DropColumn(
                name: "naPotezu",
                table: "Igra");
        }
    }
}
