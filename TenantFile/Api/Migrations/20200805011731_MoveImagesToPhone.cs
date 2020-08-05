using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class MoveImagesToPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Tenants_TenantId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_TenantId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "PhoneId",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_PhoneId",
                table: "Image",
                column: "PhoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Phones_PhoneId",
                table: "Image",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Phones_PhoneId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_PhoneId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Image",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_TenantId",
                table: "Image",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Tenants_TenantId",
                table: "Image",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
