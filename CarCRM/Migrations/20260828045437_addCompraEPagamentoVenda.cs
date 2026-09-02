using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class addCompraEPagamentoVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PagamentoVendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendaId = table.Column<int>(type: "int", nullable: false),
                    PagamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoVendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoVendas_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagamentoVendas_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorCompra = table.Column<float>(type: "real", nullable: false),
                    Desconto = table.Column<float>(type: "real", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    StatusCompraId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_StatusCompras_StatusCompraId",
                        column: x => x.StatusCompraId,
                        principalTable: "StatusCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Usuarios_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagamentoCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    PagamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoCompras_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagamentoCompras_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_StatusCompraId",
                table: "Compras",
                column: "StatusCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_VeiculoId",
                table: "Compras",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_VendedorId",
                table: "Compras",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoCompras_CompraId",
                table: "PagamentoCompras",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoCompras_PagamentoId",
                table: "PagamentoCompras",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoVendas_PagamentoId",
                table: "PagamentoVendas",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoVendas_VendaId",
                table: "PagamentoVendas",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagamentoCompras");

            migrationBuilder.DropTable(
                name: "PagamentoVendas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "StatusCompras");
        }
    }
}
