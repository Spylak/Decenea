using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Locations7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_MunicipalUnit_MunicipalUnitId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnit_Municipalities_MunicipalityId",
                table: "MunicipalUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MunicipalUnit",
                table: "MunicipalUnit");

            migrationBuilder.RenameTable(
                name: "MunicipalUnit",
                newName: "MunicipalUnits");

            migrationBuilder.RenameIndex(
                name: "IX_MunicipalUnit_MunicipalityId",
                table: "MunicipalUnits",
                newName: "IX_MunicipalUnits_MunicipalityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MunicipalUnits",
                table: "MunicipalUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_MunicipalUnits_MunicipalUnitId",
                table: "Communities",
                column: "MunicipalUnitId",
                principalTable: "MunicipalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnits_Municipalities_MunicipalityId",
                table: "MunicipalUnits",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_MunicipalUnits_MunicipalUnitId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_MunicipalUnits_Municipalities_MunicipalityId",
                table: "MunicipalUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MunicipalUnits",
                table: "MunicipalUnits");

            migrationBuilder.RenameTable(
                name: "MunicipalUnits",
                newName: "MunicipalUnit");

            migrationBuilder.RenameIndex(
                name: "IX_MunicipalUnits_MunicipalityId",
                table: "MunicipalUnit",
                newName: "IX_MunicipalUnit_MunicipalityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MunicipalUnit",
                table: "MunicipalUnit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_MunicipalUnit_MunicipalUnitId",
                table: "Communities",
                column: "MunicipalUnitId",
                principalTable: "MunicipalUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MunicipalUnit_Municipalities_MunicipalityId",
                table: "MunicipalUnit",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
