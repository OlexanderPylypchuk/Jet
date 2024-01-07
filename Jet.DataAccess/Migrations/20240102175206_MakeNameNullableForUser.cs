using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeNameNullableForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CompanyTable_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CompanyTable_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "CompanyTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CompanyTable_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CompanyTable_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "CompanyTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
