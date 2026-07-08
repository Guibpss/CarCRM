using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class ClienteComposicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remove FKs que dependem da coluna Id do Cliente
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Pessoas_Id",
                table: "Clientes");

            // 2. Remove a PK que depende da coluna Id
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            // 3. FK do Usuarios (mudança independente)
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Pessoas_PessoaId",
                table: "Usuarios");

            // 4. ADICIONA as colunas novas ANTES de dropar o Id
            //    (assim a tabela nunca fica sem colunas de dados)
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Clientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 5. AGORA pode dropar o Id (a tabela já tem outras colunas)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Clientes");

            // 6. Recria Id como IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Clientes",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // 7. Recria a PK no Id novo
            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            // 8. Índice e FK nova do Cliente -> Pessoa
            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PessoaId",
                table: "Clientes",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Pessoas_PessoaId",
                table: "Clientes",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // 9. Recria a FK da Vendas apontando pro Id novo
            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // 10. FK do Usuarios -> PessoasFisica
            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_PessoasFisica_PessoaId",
                table: "Usuarios",
                column: "PessoaId",
                principalTable: "PessoasFisica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Pessoas_PessoaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_PessoasFisica_PessoaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_PessoaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Clientes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Pessoas_Id",
                table: "Clientes",
                column: "Id",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Pessoas_PessoaId",
                table: "Usuarios",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
