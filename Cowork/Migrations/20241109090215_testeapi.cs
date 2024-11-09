using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cowork.Migrations
{
    /// <inheritdoc />
    public partial class testeapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Salas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Salas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Salas");
        }
    }
}
