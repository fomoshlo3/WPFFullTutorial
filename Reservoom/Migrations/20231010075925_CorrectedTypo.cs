using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservoom.Migrations
{
    public partial class CorrectedTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoofNumber",
                table: "Reservations",
                newName: "RoomNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "Reservations",
                newName: "RoofNumber");
        }
    }
}
