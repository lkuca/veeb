using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veeb.Migrations
{
    /// <inheritdoc />
    public partial class commitetproper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tooded_kasutajad_KasutajaId",
                table: "Tooded");

            migrationBuilder.DropIndex(
                name: "IX_Tooded_KasutajaId",
                table: "Tooded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tooded_KasutajaId",
                table: "Tooded",
                column: "KasutajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tooded_kasutajad_KasutajaId",
                table: "Tooded",
                column: "KasutajaId",
                principalTable: "kasutajad",
                principalColumn: "Id");
        }
    }
}
