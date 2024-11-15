using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cowork.Migrations
{
    /// <inheritdoc />
    public partial class functeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvisoExclusaoFuncionario",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvisoExclusaoFuncionario",
                table: "Reservas");
        }
    }
}
