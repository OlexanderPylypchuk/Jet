using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemakeAdnSeedFilmTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FilmTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "CategoryId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_FilmTable_CategoryId",
                table: "FilmTable",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmTable_CategoryTable_CategoryId",
                table: "FilmTable",
                column: "CategoryId",
                principalTable: "CategoryTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmTable_CategoryTable_CategoryId",
                table: "FilmTable");

            migrationBuilder.DropIndex(
                name: "IX_FilmTable_CategoryId",
                table: "FilmTable");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FilmTable");
        }
    }
}
