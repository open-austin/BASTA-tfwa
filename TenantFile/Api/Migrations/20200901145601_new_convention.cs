using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantFile.Api.Migrations
{
    public partial class new_convention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Phones_PhoneId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Address_AddressId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Residence_Properties_ResidenceIdId",
                table: "Residence");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceRecord_Residence_ResidenceRecordId",
                table: "ResidenceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceRecord_Tenants_TenantId",
                table: "ResidenceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantPhone_Phones_PhoneId",
                table: "TenantPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantPhone_Tenants_TenantId",
                table: "TenantPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Residence",
                table: "Residence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phones",
                table: "Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantPhone",
                table: "TenantPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResidenceRecord",
                table: "ResidenceRecord");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "tenants");

            migrationBuilder.RenameTable(
                name: "Residence",
                newName: "residence");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "properties");

            migrationBuilder.RenameTable(
                name: "Phones",
                newName: "phones");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "image");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "address");

            migrationBuilder.RenameTable(
                name: "TenantPhone",
                newName: "tenant_phone");

            migrationBuilder.RenameTable(
                name: "ResidenceRecord",
                newName: "residence_record");

            migrationBuilder.RenameColumn(
                name: "TenantName",
                table: "tenants",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "tenants",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UnitIdentifier",
                table: "residence",
                newName: "unit_identifier");

            migrationBuilder.RenameColumn(
                name: "ResidenceIdId",
                table: "residence",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UnitIdentifier",
                table: "properties",
                newName: "unit_identifier");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "properties",
                newName: "address_id");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "properties",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_AddressId",
                table: "properties",
                newName: "ix_properties_address_id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "phones",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PhoneId",
                table: "phones",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "image",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ThumbnailName",
                table: "image",
                newName: "thumbnail_name");

            migrationBuilder.RenameColumn(
                name: "PhoneId",
                table: "image",
                newName: "phone_id");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "image",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Image_PhoneId",
                table: "image",
                newName: "ix_image_phone_id");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "address",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "address",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "address",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "address",
                newName: "street_number");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "address",
                newName: "postal_code");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "address",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhoneId",
                table: "tenant_phone",
                newName: "phone_id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "tenant_phone",
                newName: "tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_TenantPhone_PhoneId",
                table: "tenant_phone",
                newName: "ix_tenant_phone_phone_id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "residence_record",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "MoveOut",
                table: "residence_record",
                newName: "move_out");

            migrationBuilder.RenameColumn(
                name: "MoveIn",
                table: "residence_record",
                newName: "move_in");

            migrationBuilder.RenameColumn(
                name: "ResidenceRecordId",
                table: "residence_record",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ResidenceRecord_TenantId",
                table: "residence_record",
                newName: "ix_residence_record_tenant_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tenants",
                table: "tenants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_residence",
                table: "residence",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_properties",
                table: "properties",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_phones",
                table: "phones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_image",
                table: "image",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address",
                table: "address",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tenant_phone",
                table: "tenant_phone",
                columns: new[] { "tenant_id", "phone_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_residence_record",
                table: "residence_record",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_image_phones_phone_id",
                table: "image",
                column: "phone_id",
                principalTable: "phones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_properties_address_address_id",
                table: "properties",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_residence_properties_property_id",
                table: "residence",
                column: "id",
                principalTable: "properties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_residence_record_residence_residence_id",
                table: "residence_record",
                column: "id",
                principalTable: "residence",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_residence_record_tenants_tenant_id",
                table: "residence_record",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tenant_phone_phones_phone_id",
                table: "tenant_phone",
                column: "phone_id",
                principalTable: "phones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tenant_phone_tenants_tenant_id",
                table: "tenant_phone",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_image_phones_phone_id",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "fk_properties_address_address_id",
                table: "properties");

            migrationBuilder.DropForeignKey(
                name: "fk_residence_properties_property_id",
                table: "residence");

            migrationBuilder.DropForeignKey(
                name: "fk_residence_record_residence_residence_id",
                table: "residence_record");

            migrationBuilder.DropForeignKey(
                name: "fk_residence_record_tenants_tenant_id",
                table: "residence_record");

            migrationBuilder.DropForeignKey(
                name: "fk_tenant_phone_phones_phone_id",
                table: "tenant_phone");

            migrationBuilder.DropForeignKey(
                name: "fk_tenant_phone_tenants_tenant_id",
                table: "tenant_phone");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tenants",
                table: "tenants");

            migrationBuilder.DropPrimaryKey(
                name: "pk_residence",
                table: "residence");

            migrationBuilder.DropPrimaryKey(
                name: "pk_properties",
                table: "properties");

            migrationBuilder.DropPrimaryKey(
                name: "pk_phones",
                table: "phones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_image",
                table: "image");

            migrationBuilder.DropPrimaryKey(
                name: "pk_address",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tenant_phone",
                table: "tenant_phone");

            migrationBuilder.DropPrimaryKey(
                name: "pk_residence_record",
                table: "residence_record");

            migrationBuilder.RenameTable(
                name: "tenants",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "residence",
                newName: "Residence");

            migrationBuilder.RenameTable(
                name: "properties",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "phones",
                newName: "Phones");

            migrationBuilder.RenameTable(
                name: "image",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "Address");

            migrationBuilder.RenameTable(
                name: "tenant_phone",
                newName: "TenantPhone");

            migrationBuilder.RenameTable(
                name: "residence_record",
                newName: "ResidenceRecord");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tenants",
                newName: "TenantName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tenants",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "unit_identifier",
                table: "Residence",
                newName: "UnitIdentifier");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Residence",
                newName: "ResidenceIdId");

            migrationBuilder.RenameColumn(
                name: "unit_identifier",
                table: "Properties",
                newName: "UnitIdentifier");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "Properties",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Properties",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "ix_properties_address_id",
                table: "Properties",
                newName: "IX_Properties_AddressId");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Phones",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Phones",
                newName: "PhoneId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Image",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "thumbnail_name",
                table: "Image",
                newName: "ThumbnailName");

            migrationBuilder.RenameColumn(
                name: "phone_id",
                table: "Image",
                newName: "PhoneId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Image",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "ix_image_phone_id",
                table: "Image",
                newName: "IX_Image_PhoneId");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "Address",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Address",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Address",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "street_number",
                table: "Address",
                newName: "StreetNumber");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "Address",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Address",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "phone_id",
                table: "TenantPhone",
                newName: "PhoneId");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "TenantPhone",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "ix_tenant_phone_phone_id",
                table: "TenantPhone",
                newName: "IX_TenantPhone_PhoneId");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "ResidenceRecord",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "move_out",
                table: "ResidenceRecord",
                newName: "MoveOut");

            migrationBuilder.RenameColumn(
                name: "move_in",
                table: "ResidenceRecord",
                newName: "MoveIn");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ResidenceRecord",
                newName: "ResidenceRecordId");

            migrationBuilder.RenameIndex(
                name: "ix_residence_record_tenant_id",
                table: "ResidenceRecord",
                newName: "IX_ResidenceRecord_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Residence",
                table: "Residence",
                column: "ResidenceIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phones",
                table: "Phones",
                column: "PhoneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantPhone",
                table: "TenantPhone",
                columns: new[] { "TenantId", "PhoneId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResidenceRecord",
                table: "ResidenceRecord",
                column: "ResidenceRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Phones_PhoneId",
                table: "Image",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "PhoneId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Address_AddressId",
                table: "Properties",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residence_Properties_ResidenceIdId",
                table: "Residence",
                column: "ResidenceIdId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceRecord_Residence_ResidenceRecordId",
                table: "ResidenceRecord",
                column: "ResidenceRecordId",
                principalTable: "Residence",
                principalColumn: "ResidenceIdId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceRecord_Tenants_TenantId",
                table: "ResidenceRecord",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantPhone_Phones_PhoneId",
                table: "TenantPhone",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "PhoneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantPhone_Tenants_TenantId",
                table: "TenantPhone",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
