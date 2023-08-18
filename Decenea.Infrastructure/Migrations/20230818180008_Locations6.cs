using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Locations6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_MunicipalUnit_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Prefectures_PrefectureId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Cities_SeatId",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Countries_CountryId",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Prefectures_PrefectureId",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Regions_RegionId",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnit_Countries_CountryId",
                table: "MunicipalUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnit_Prefectures_PrefectureId",
                table: "MunicipalUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnit_Regions_RegionId",
                table: "MunicipalUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Cities_CapitalId",
                table: "Regions");

            migrationBuilder.DropTable(
                name: "Prefectures");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CapitalId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Name",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_MunicipalUnit_CountryId",
                table: "MunicipalUnit");

            migrationBuilder.DropIndex(
                name: "IX_MunicipalUnit_PrefectureId",
                table: "MunicipalUnit");

            migrationBuilder.DropIndex(
                name: "IX_MunicipalUnit_RegionId",
                table: "MunicipalUnit");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_CountryId",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_Name",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_PrefectureId",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_SeatId",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_PrefectureId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "MunicipalUnit");

            migrationBuilder.DropColumn(
                name: "PrefectureId",
                table: "MunicipalUnit");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "MunicipalUnit");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "PrefectureId",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "MunicipalUnitId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PrefectureId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Municipalities",
                newName: "RegionalUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipalities_RegionId",
                table: "Municipalities",
                newName: "IX_Municipalities_RegionalUnitId");

            migrationBuilder.AddColumn<string>(
                name: "CommunityId",
                table: "Cities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AsciiName = table.Column<string>(type: "text", nullable: true),
                    AlternativeName = table.Column<string>(type: "text", nullable: true),
                    MunicipalUnitId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_MunicipalUnit_MunicipalUnitId",
                        column: x => x.MunicipalUnitId,
                        principalTable: "MunicipalUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionalUnits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AsciiName = table.Column<string>(type: "text", nullable: true),
                    AlternativeName = table.Column<string>(type: "text", nullable: true),
                    RegionId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionalUnits_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CommunityId",
                table: "Cities",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_MunicipalUnitId",
                table: "Communities",
                column: "MunicipalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalUnits_RegionId",
                table: "RegionalUnits",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Communities_CommunityId",
                table: "Cities",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_RegionalUnits_RegionalUnitId",
                table: "Municipalities",
                column: "RegionalUnitId",
                principalTable: "RegionalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Communities_CommunityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_RegionalUnits_RegionalUnitId",
                table: "Municipalities");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "RegionalUnits");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CommunityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "RegionalUnitId",
                table: "Municipalities",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipalities_RegionalUnitId",
                table: "Municipalities",
                newName: "IX_Municipalities_RegionId");

            migrationBuilder.AddColumn<string>(
                name: "CapitalId",
                table: "Regions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "MunicipalUnit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrefectureId",
                table: "MunicipalUnit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegionId",
                table: "MunicipalUnit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Municipalities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrefectureId",
                table: "Municipalities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatId",
                table: "Municipalities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MunicipalUnitId",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MunicipalityId",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrefectureId",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegionId",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Prefectures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<string>(type: "text", nullable: false),
                    AlternativeName = table.Column<string>(type: "text", nullable: true),
                    AsciiName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prefectures_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prefectures_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CapitalId",
                table: "Regions",
                column: "CapitalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_CountryId",
                table: "MunicipalUnit",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_PrefectureId",
                table: "MunicipalUnit",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_RegionId",
                table: "MunicipalUnit",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_CountryId",
                table: "Municipalities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_Name",
                table: "Municipalities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_PrefectureId",
                table: "Municipalities",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_SeatId",
                table: "Municipalities",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_PrefectureId",
                table: "Cities",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefectures_CountryId",
                table: "Prefectures",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefectures_Name",
                table: "Prefectures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prefectures_RegionId",
                table: "Prefectures",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_MunicipalUnit_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId",
                principalTable: "MunicipalUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Prefectures_PrefectureId",
                table: "Cities",
                column: "PrefectureId",
                principalTable: "Prefectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "Cities",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Cities_SeatId",
                table: "Municipalities",
                column: "SeatId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Countries_CountryId",
                table: "Municipalities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Prefectures_PrefectureId",
                table: "Municipalities",
                column: "PrefectureId",
                principalTable: "Prefectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Regions_RegionId",
                table: "Municipalities",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnit_Countries_CountryId",
                table: "MunicipalUnit",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnit_Prefectures_PrefectureId",
                table: "MunicipalUnit",
                column: "PrefectureId",
                principalTable: "Prefectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnit_Regions_RegionId",
                table: "MunicipalUnit",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Cities_CapitalId",
                table: "Regions",
                column: "CapitalId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
