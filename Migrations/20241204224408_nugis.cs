using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veeb.Migrations
{
    /// <inheritdoc />
    public partial class nugis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KasutajaId",
                table: "Tooted",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tooted_KasutajaId",
                table: "Tooted",
                column: "KasutajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tooted_Kasutajad_KasutajaId",
                table: "Tooted",
                column: "KasutajaId",
                principalTable: "Kasutajad",
                principalColumn: "KasutajaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tooted_Kasutajad_KasutajaId",
                table: "Tooted");

            migrationBuilder.DropIndex(
                name: "IX_Tooted_KasutajaId",
                table: "Tooted");

            migrationBuilder.DropColumn(
                name: "KasutajaId",
                table: "Tooted");
        }
    }
}
