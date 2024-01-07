using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInFilmTableAndInitialisationOfFilmImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "FilmTable");

            migrationBuilder.CreateTable(
                name: "FilmImageTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmImageTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmImageTable_FilmTable_FilmId",
                        column: x => x.FilmId,
                        principalTable: "FilmTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmImageTable_FilmId",
                table: "FilmImageTable",
                column: "FilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmImageTable");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "FilmTable",
                type: "nvarchar(max)",
                nullable: true);

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
                keyValue: 3,
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
    }
}
