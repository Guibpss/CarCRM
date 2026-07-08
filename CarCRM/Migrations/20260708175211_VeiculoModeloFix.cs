using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class VeiculoModeloFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_veiculoMarcas_VeiculoModeloId",
                table: "Veiculos");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_VeiculoModelo_VeiculoModeloId",
                table: "Veiculos",
                column: "VeiculoModeloId",
                principalTable: "VeiculoModelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_VeiculoModelo_VeiculoModeloId",
                table: "Veiculos");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_veiculoMarcas_VeiculoModeloId",
                table: "Veiculos",
                column: "VeiculoModeloId",
                principalTable: "veiculoMarcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
