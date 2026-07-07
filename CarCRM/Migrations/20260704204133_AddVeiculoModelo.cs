using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class AddVeiculoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VeiculoModeloId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_VeiculoModeloId",
                table: "Veiculos",
                column: "VeiculoModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_veiculoMarcas_VeiculoModeloId",
                table: "Veiculos",
                column: "VeiculoModeloId",
                principalTable: "veiculoMarcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_veiculoMarcas_VeiculoModeloId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_VeiculoModeloId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoModeloId",
                table: "Veiculos");
        }
    }
}
