using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class RenamePecasParaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Pecas",
                newName: "Produtos");

            migrationBuilder.Sql("EXEC sp_rename N'PK_Pecas', N'PK_Produtos', N'OBJECT';");

            migrationBuilder.Sql("EXEC sp_rename N'FK_Pecas_Clientes_ClienteId', N'FK_Produtos_Clientes_ClienteId', N'OBJECT';");

            migrationBuilder.RenameIndex(
                name: "IX_Pecas_ClienteId",
                table: "Produtos",
                newName: "IX_Produtos_ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos",
                newName: "IX_Pecas_ClienteId");

            migrationBuilder.Sql("EXEC sp_rename N'FK_Produtos_Clientes_ClienteId', N'FK_Pecas_Clientes_ClienteId', N'OBJECT';");

            migrationBuilder.Sql("EXEC sp_rename N'PK_Produtos', N'PK_Pecas', N'OBJECT';");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "Pecas");
        }
    }
}
