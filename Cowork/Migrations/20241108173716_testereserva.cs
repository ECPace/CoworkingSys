using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cowork.Migrations
{
    /// <inheritdoc />
    public partial class testereserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioReserva_Funcionarios_FuncionariosId",
                table: "FuncionarioReserva");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioReserva_Reservas_ReservasId",
                table: "FuncionarioReserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FuncionarioReserva",
                table: "FuncionarioReserva");

            migrationBuilder.RenameTable(
                name: "FuncionarioReserva",
                newName: "ReservaFuncionarios");

            migrationBuilder.RenameIndex(
                name: "IX_FuncionarioReserva_ReservasId",
                table: "ReservaFuncionarios",
                newName: "IX_ReservaFuncionarios_ReservasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservaFuncionarios",
                table: "ReservaFuncionarios",
                columns: new[] { "FuncionariosId", "ReservasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaFuncionarios_Funcionarios_FuncionariosId",
                table: "ReservaFuncionarios",
                column: "FuncionariosId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaFuncionarios_Reservas_ReservasId",
                table: "ReservaFuncionarios",
                column: "ReservasId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservaFuncionarios_Funcionarios_FuncionariosId",
                table: "ReservaFuncionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservaFuncionarios_Reservas_ReservasId",
                table: "ReservaFuncionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservaFuncionarios",
                table: "ReservaFuncionarios");

            migrationBuilder.RenameTable(
                name: "ReservaFuncionarios",
                newName: "FuncionarioReserva");

            migrationBuilder.RenameIndex(
                name: "IX_ReservaFuncionarios_ReservasId",
                table: "FuncionarioReserva",
                newName: "IX_FuncionarioReserva_ReservasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuncionarioReserva",
                table: "FuncionarioReserva",
                columns: new[] { "FuncionariosId", "ReservasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioReserva_Funcionarios_FuncionariosId",
                table: "FuncionarioReserva",
                column: "FuncionariosId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioReserva_Reservas_ReservasId",
                table: "FuncionarioReserva",
                column: "ReservasId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
