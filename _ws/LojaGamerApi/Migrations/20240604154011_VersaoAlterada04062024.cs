using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaGamerApi.Migrations
{
    /// <inheritdoc />
    public partial class VersaoAlterada04062024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteCompra",
                columns: table => new
                {
                    clientesId = table.Column<int>(type: "int", nullable: false),
                    comprasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteCompra", x => new { x.clientesId, x.comprasId });
                    table.ForeignKey(
                        name: "FK_ClienteCompra_Cliente_clientesId",
                        column: x => x.clientesId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteCompra_Compra_comprasId",
                        column: x => x.comprasId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GamesVenda",
                columns: table => new
                {
                    gamesId = table.Column<int>(type: "int", nullable: false),
                    vendasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesVenda", x => new { x.gamesId, x.vendasId });
                    table.ForeignKey(
                        name: "FK_GamesVenda_Games_gamesId",
                        column: x => x.gamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesVenda_Venda_vendasId",
                        column: x => x.vendasId,
                        principalTable: "Venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteCompra_comprasId",
                table: "ClienteCompra",
                column: "comprasId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesVenda_vendasId",
                table: "GamesVenda",
                column: "vendasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteCompra");

            migrationBuilder.DropTable(
                name: "GamesVenda");
        }
    }
}
