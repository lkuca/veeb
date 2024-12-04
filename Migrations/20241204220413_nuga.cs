using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veeb.Migrations
{
    /// <inheritdoc />
    public partial class nuga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kasutajad",
                newName: "KasutajaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KasutajaId",
                table: "Kasutajad",
                newName: "Id");
        }
    }
}
