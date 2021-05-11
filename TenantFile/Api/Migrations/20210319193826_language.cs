using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class language : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredLanuage",
                table: "Phones",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredLanuage",
                table: "Phones");
        }
    }
}
