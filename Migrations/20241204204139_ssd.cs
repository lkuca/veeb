using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veeb.Migrations
{
    /// <inheritdoc />
    public partial class ssd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tooded_kasutajad_KasutajaId",
                table: "Tooded");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kasutajad",
                table: "kasutajad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tooded",
                table: "Tooded");

            migrationBuilder.RenameTable(
                name: "kasutajad",
                newName: "Kasutajad");

            migrationBuilder.RenameTable(
                name: "Tooded",
                newName: "Tooted");

            migrationBuilder.RenameColumn(
                name: "KasutajaId",
                table: "Tooted",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Tooted",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_Tooded_KasutajaId",
                table: "Tooted",
                newName: "IX_Tooted_CartId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Kasutajad",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kasutajad",
                table: "Kasutajad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tooted",
                table: "Tooted",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KasutajaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kasutajad_CartId",
                table: "Kasutajad",
                column: "CartId",
                unique: true,
                filter: "[CartId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tooted_Carts_CartId",
                table: "Tooted",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kasutajad_Carts_CartId",
                table: "Kasutajad");

            migrationBuilder.DropForeignKey(
                name: "FK_Tooted_Carts_CartId",
                table: "Tooted");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kasutajad",
                table: "Kasutajad");

            migrationBuilder.DropIndex(
                name: "IX_Kasutajad_CartId",
                table: "Kasutajad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tooted",
                table: "Tooted");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Kasutajad");

            migrationBuilder.RenameTable(
                name: "Kasutajad",
                newName: "kasutajad");

            migrationBuilder.RenameTable(
                name: "Tooted",
                newName: "Tooded");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Tooded",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Tooded",
                newName: "KasutajaId");

            migrationBuilder.RenameIndex(
                name: "IX_Tooted_CartId",
                table: "Tooded",
                newName: "IX_Tooded_KasutajaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kasutajad",
                table: "kasutajad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tooded",
                table: "Tooded",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tooded_kasutajad_KasutajaId",
                table: "Tooded",
                column: "KasutajaId",
                principalTable: "kasutajad",
                principalColumn: "Id");
        }
    }
}
