using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlToFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "FilmTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImgUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgUrl",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "FilmTable");
        }
    }
}
