using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class organizers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "organizer_uid",
                table: "properties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "organizers",
                columns: table => new
                {
                    uid = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizers", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "organizer_phone",
                columns: table => new
                {
                    organizer_id = table.Column<string>(nullable: false),
                    phone_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizer_phone", x => new { x.organizer_id, x.phone_id });
                    table.ForeignKey(
                        name: "fk_organizer_phone_organizers_organizer_id",
                        column: x => x.organizer_id,
                        principalTable: "organizers",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizer_phone_phones_phone_id",
                        column: x => x.phone_id,
                        principalTable: "phones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_properties_organizer_uid",
                table: "properties",
                column: "organizer_uid");

            migrationBuilder.CreateIndex(
                name: "ix_organizer_phone_phone_id",
                table: "organizer_phone",
                column: "phone_id");

            migrationBuilder.AddForeignKey(
                name: "fk_properties_organizers_organizer_uid",
                table: "properties",
                column: "organizer_uid",
                principalTable: "organizers",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_properties_organizers_organizer_uid",
                table: "properties");

            migrationBuilder.DropTable(
                name: "organizer_phone");

            migrationBuilder.DropTable(
                name: "organizers");

            migrationBuilder.DropIndex(
                name: "ix_properties_organizer_uid",
                table: "properties");

            migrationBuilder.DropColumn(
                name: "organizer_uid",
                table: "properties");
        }
    }
}
