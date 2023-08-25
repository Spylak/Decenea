using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Locations5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeatId",
                table: "Municipalities",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "Cities",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Cities",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "MunicipalUnitId",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MunicipalUnit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AsciiName = table.Column<string>(type: "text", nullable: true),
                    AlternativeName = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<string>(type: "text", nullable: false),
                    PrefectureId = table.Column<string>(type: "text", nullable: false),
                    MunicipalityId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MunicipalUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MunicipalUnit_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MunicipalUnit_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MunicipalUnit_Prefectures_PrefectureId",
                        column: x => x.PrefectureId,
                        principalTable: "Prefectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MunicipalUnit_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_SeatId",
                table: "Municipalities",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_CountryId",
                table: "MunicipalUnit",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_MunicipalityId",
                table: "MunicipalUnit",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_PrefectureId",
                table: "MunicipalUnit",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalUnit_RegionId",
                table: "MunicipalUnit",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_MunicipalUnit_MunicipalityId",
                table: "Cities",
                column: "MunicipalityId",
                principalTable: "MunicipalUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Cities_SeatId",
                table: "Municipalities",
                column: "SeatId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_MunicipalUnit_MunicipalityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Cities_SeatId",
                table: "Municipalities");

            migrationBuilder.DropTable(
                name: "MunicipalUnit");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_SeatId",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "MunicipalUnitId",
                table: "Cities");

            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "Cities",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Cities",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
