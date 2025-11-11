using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstFlyProject.Migrations
{
    /// <inheritdoc />
    public partial class addeddestinationtotravelpackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "TravelPackages");
        }
    }
}
