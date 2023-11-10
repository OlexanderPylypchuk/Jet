using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedFilmTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgUrl",
                value: "/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgUrl",
                value: "/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgUrl",
                value: "/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgUrl",
                value: "/images/film/default.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgUrl",
                value: "~/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgUrl",
                value: "~/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgUrl",
                value: "~/images/film/default.jpg");

            migrationBuilder.UpdateData(
                table: "FilmTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgUrl",
                value: "~/images/film/default.jpg");
        }
    }
}
