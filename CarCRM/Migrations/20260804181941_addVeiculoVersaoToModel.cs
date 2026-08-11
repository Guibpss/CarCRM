using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class addVeiculoVersaoToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VeiculoVersaoId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_VeiculoVersaoId",
                table: "Veiculos",
                column: "VeiculoVersaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_VeiculoVersao_VeiculoVersaoId",
                table: "Veiculos",
                column: "VeiculoVersaoId",
                principalTable: "VeiculoVersao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_VeiculoVersao_VeiculoVersaoId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_VeiculoVersaoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoVersaoId",
                table: "Veiculos");
        }
    }
}
