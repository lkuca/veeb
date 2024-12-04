using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veeb.Migrations
{
    /// <inheritdoc />
    public partial class ser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad");

            migrationBuilder.AddForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad");

            migrationBuilder.AddForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
