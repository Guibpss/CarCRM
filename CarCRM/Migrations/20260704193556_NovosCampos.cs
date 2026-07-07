using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class NovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Funcionarios_FuncionarioId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Combustivel",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Motorizacao",
                table: "Veiculos");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "Vendas",
                newName: "VendedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_FuncionarioId",
                table: "Vendas",
                newName: "IX_Vendas_VendedorId");

            migrationBuilder.AlterColumn<int>(
                name: "AnoModelo",
                table: "Veiculos",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "AnoFabricacao",
                table: "Veiculos",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoCombustivelId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VeiculoMotorizacaoId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmaSenha",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VeiculoCombustivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoCombustivel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoMotorizacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoMotorizacao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_VeiculoCombustivelId",
                table: "Veiculos",
                column: "VeiculoCombustivelId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_VeiculoMotorizacaoId",
                table: "Veiculos",
                column: "VeiculoMotorizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_VeiculoCombustivel_VeiculoCombustivelId",
                table: "Veiculos",
                column: "VeiculoCombustivelId",
                principalTable: "VeiculoCombustivel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_VeiculoMotorizacao_VeiculoMotorizacaoId",
                table: "Veiculos",
                column: "VeiculoMotorizacaoId",
                principalTable: "VeiculoMotorizacao",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_VeiculoCombustivel_VeiculoCombustivelId",
                table: "Veiculos");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_VeiculoMotorizacao_VeiculoMotorizacaoId",
                table: "Veiculos");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Usuarios_VendedorId",
                table: "Vendas");

            migrationBuilder.DropTable(
                name: "VeiculoCombustivel");

            migrationBuilder.DropTable(
                name: "VeiculoMotorizacao");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_VeiculoCombustivelId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_VeiculoMotorizacaoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoCombustivelId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoMotorizacaoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "ConfirmaSenha",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "VendedorId",
                table: "Vendas",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas",
                newName: "IX_Vendas_FuncionarioId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AnoModelo",
                table: "Veiculos",
                type: "date",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AnoFabricacao",
                table: "Veiculos",
                type: "date",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Combustivel",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Motorizacao",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Funcionarios_FuncionarioId",
                table: "Vendas",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
