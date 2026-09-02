using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCRM.Migrations.CarCRMImagem
{
    /// <inheritdoc />
    public partial class UsuarioImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioImagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioImagem", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioImagem");
        }
    }
}
