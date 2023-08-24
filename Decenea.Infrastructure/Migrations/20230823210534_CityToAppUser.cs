using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CityToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroAd_Cities_CityId",
                table: "MicroAd");

            migrationBuilder.DropForeignKey(
                name: "FK_MicroAd_Users_ApplicationUserId",
                table: "MicroAd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MicroAd",
                table: "MicroAd");

            migrationBuilder.RenameTable(
                name: "MicroAd",
                newName: "MicroAds");

            migrationBuilder.RenameIndex(
                name: "IX_MicroAd_CityId",
                table: "MicroAds",
                newName: "IX_MicroAds_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_MicroAd_ApplicationUserId",
                table: "MicroAds",
                newName: "IX_MicroAds_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicroAds",
                table: "MicroAds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroAds_Cities_CityId",
                table: "MicroAds",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MicroAds_Users_ApplicationUserId",
                table: "MicroAds",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroAds_Cities_CityId",
                table: "MicroAds");

            migrationBuilder.DropForeignKey(
                name: "FK_MicroAds_Users_ApplicationUserId",
                table: "MicroAds");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MicroAds",
                table: "MicroAds");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "MicroAds",
                newName: "MicroAd");

            migrationBuilder.RenameIndex(
                name: "IX_MicroAds_CityId",
                table: "MicroAd",
                newName: "IX_MicroAd_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_MicroAds_ApplicationUserId",
                table: "MicroAd",
                newName: "IX_MicroAd_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicroAd",
                table: "MicroAd",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroAd_Cities_CityId",
                table: "MicroAd",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MicroAd_Users_ApplicationUserId",
                table: "MicroAd",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
