using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations
{
    /// <inheritdoc />
    public partial class addRenavamTransmissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoTransmissaoId",
                table: "Veiculos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VeiculoTransmissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoTransmissao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_VeiculoTransmissaoId",
                table: "Veiculos",
                column: "VeiculoTransmissaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_VeiculoTransmissao_VeiculoTransmissaoId",
                table: "Veiculos",
                column: "VeiculoTransmissaoId",
                principalTable: "VeiculoTransmissao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_VeiculoTransmissao_VeiculoTransmissaoId",
                table: "Veiculos");

            migrationBuilder.DropTable(
                name: "VeiculoTransmissao");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_VeiculoTransmissaoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Renavam",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoTransmissaoId",
                table: "Veiculos");
        }
    }
}
