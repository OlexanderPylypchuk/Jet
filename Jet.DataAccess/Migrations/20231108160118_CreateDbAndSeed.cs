using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmTable_CategoryTable_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryTable",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "Sci-Fi" },
                    { 3, 3, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "FilmTable",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUrl", "Price", "Producer", "Score", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Some text about Avatar", "~/images/film/default.jpg", 340, "Some dude", 9.8000000000000007, "Avatar" },
                    { 2, 2, "Some text about Avatar 2", "~/images/film/default.jpg", 400, "Some dude", 10.0, "Avatar 2" },
                    { 3, 3, "Some text about Avatar: The last airbender", "~/images/film/default.jpg", 340, "Some dude 2", 8.6999999999999993, "Avatar: The last airbender" },
                    { 4, 1, "Some text about Drive", "~/images/film/default.jpg", 540, "Some dude", 7.0, "Drive" },
                    { 5, 2, "Some text about Drive", "~/images/film/default.jpg", 540, "Some dude", 7.0, "Drive" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmTable_CategoryId",
                table: "FilmTable",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmTable");

            migrationBuilder.DropTable(
                name: "CategoryTable");
        }
    }
}
