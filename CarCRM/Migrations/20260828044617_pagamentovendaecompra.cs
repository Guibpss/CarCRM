using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class pagamentovendaecompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Veiculos_VeiculoId",
                table: "Pagamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Usuarios_VendedorId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_VeiculoId",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Pagamentos");

            migrationBuilder.RenameColumn(
                name: "VendedorId",
                table: "Vendas",
                newName: "VeiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas",
                newName: "IX_Vendas_VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoId",
                table: "Vendas",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoId",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "VeiculoId",
                table: "Vendas",
                newName: "VendedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VeiculoId",
                table: "Vendas",
                newName: "IX_Vendas_VendedorId");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "Pagamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_VeiculoId",
                table: "Pagamentos",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Veiculos_VeiculoId",
                table: "Pagamentos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Usuarios_VendedorId",
                table: "Vendas",
                column: "VendedorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
