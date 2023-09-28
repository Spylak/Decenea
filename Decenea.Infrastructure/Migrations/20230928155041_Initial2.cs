using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CountryId",
                table: "Regions",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RegionId",
                table: "RegionalUnits",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MunicipalityId",
                table: "MunicipalUnits",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RegionalUnitId",
                table: "Municipalities",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MunicipalUnitId",
                table: "Communities",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CommunityId",
                table: "Cities",
                type: "character varying(26)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalUnits_RegionId",
                table: "RegionalUnits",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnits_MunicipalityId",
                table: "MunicipalUnits",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_RegionalUnitId",
                table: "Municipalities",
                column: "RegionalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_MunicipalUnitId",
                table: "Communities",
                column: "MunicipalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CommunityId",
                table: "Cities",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Communities_CommunityId",
                table: "Cities",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_MunicipalUnits_MunicipalUnitId",
                table: "Communities",
                column: "MunicipalUnitId",
                principalTable: "MunicipalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_RegionalUnits_RegionalUnitId",
                table: "Municipalities",
                column: "RegionalUnitId",
                principalTable: "RegionalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnits_Municipalities_MunicipalityId",
                table: "MunicipalUnits",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionalUnits_Regions_RegionId",
                table: "RegionalUnits",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Communities_CommunityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_MunicipalUnits_MunicipalUnitId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_RegionalUnits_RegionalUnitId",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnits_Municipalities_MunicipalityId",
                table: "MunicipalUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionalUnits_Regions_RegionId",
                table: "RegionalUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CountryId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_RegionalUnits_RegionId",
                table: "RegionalUnits");

            migrationBuilder.DropIndex(
                name: "IX_MunicipalUnits_MunicipalityId",
                table: "MunicipalUnits");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_RegionalUnitId",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_MunicipalUnitId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CommunityId",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "CountryId",
                table: "Regions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AlterColumn<string>(
                name: "RegionId",
                table: "RegionalUnits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AlterColumn<string>(
                name: "MunicipalityId",
                table: "MunicipalUnits",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AlterColumn<string>(
                name: "RegionalUnitId",
                table: "Municipalities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AlterColumn<string>(
                name: "MunicipalUnitId",
                table: "Communities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AlterColumn<string>(
                name: "CommunityId",
                table: "Cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldNullable: true);
        }
    }
}
