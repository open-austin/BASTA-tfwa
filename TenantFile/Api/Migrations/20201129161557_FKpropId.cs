using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class FKpropId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residences_Properties_PropertyId1",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Residences_PropertyId1",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "Residences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId1",
                table: "Residences",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Residences_PropertyId1",
                table: "Residences",
                column: "PropertyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Residences_Properties_PropertyId1",
                table: "Residences",
                column: "PropertyId1",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
