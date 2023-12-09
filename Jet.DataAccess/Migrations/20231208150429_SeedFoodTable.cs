using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodTable",
                columns: new[] { "Id", "Amount", "ImgUrl", "IsFluid", "Name", "Price" },
                values: new object[] { -1, 0.0, null, false, "None", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodTable",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
