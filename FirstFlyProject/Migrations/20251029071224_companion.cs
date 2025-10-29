using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstFlyProject.Migrations
{
    /// <inheritdoc />
    public partial class companion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companions_Bookings_BookingId",
                table: "Companions");

            migrationBuilder.DropIndex(
                name: "IX_Companions_BookingId",
                table: "Companions");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Companions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Companions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companions_BookingId",
                table: "Companions",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companions_Bookings_BookingId",
                table: "Companions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");
        }
    }
}
