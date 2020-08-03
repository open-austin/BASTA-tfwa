using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class AddPropertyToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Address_AddressId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Residence_Property_Id",
                table: "Residence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Properties");

            migrationBuilder.RenameIndex(
                name: "IX_Property_AddressId",
                table: "Properties",
                newName: "IX_Properties_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Address_AddressId",
                table: "Properties",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_Properties_Id",
                table: "Residence",
                column: "Id",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Address_AddressId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Residence_Properties_Id",
                table: "Residence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Property");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_AddressId",
                table: "Property",
                newName: "IX_Property_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Address_AddressId",
                table: "Property",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_Property_Id",
                table: "Residence",
                column: "Id",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
