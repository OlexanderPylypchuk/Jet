using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedFluidOrNotState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFluid",
                table: "FoodTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFluid",
                table: "FoodTable");
        }
    }
}
