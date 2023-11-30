using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTable_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTable_FilmTable_FilmId",
                        column: x => x.FilmId,
                        principalTable: "FilmTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTable_FoodTable_FoodId",
                        column: x => x.FoodId,
                        principalTable: "FoodTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTable_ApplicationUserId",
                table: "TicketTable",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTable_FilmId",
                table: "TicketTable",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTable_FoodId",
                table: "TicketTable",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTable");
        }
    }
}
