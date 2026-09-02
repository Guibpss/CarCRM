using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCompraVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "Pagamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Pagamentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_CompraId",
                table: "Pagamentos",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_VendaId",
                table: "Pagamentos",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Compras_CompraId",
                table: "Pagamentos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Vendas_VendaId",
                table: "Pagamentos",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Compras_CompraId",
                table: "Pagamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Vendas_VendaId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_CompraId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_VendaId",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Pagamentos");
        }
    }
}
