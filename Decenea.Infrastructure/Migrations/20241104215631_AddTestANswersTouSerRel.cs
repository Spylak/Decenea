using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestANswersTouSerRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_TestUsers_TestUserTestId_TestUserUserId",
                table: "TestAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestUsers",
                table: "TestUsers");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_TestUserTestId_TestUserUserId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "TestUserTestId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "TestUserUserId",
                table: "TestAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TestUsers",
                type: "character varying(26)",
                maxLength: 26,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TestUserId",
                table: "TestAnswers",
                type: "character varying(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestUsers",
                table: "TestUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TestUsers_TestId",
                table: "TestUsers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestUserId",
                table: "TestAnswers",
                column: "TestUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_TestUsers_TestUserId",
                table: "TestAnswers",
                column: "TestUserId",
                principalTable: "TestUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_TestUsers_TestUserId",
                table: "TestAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestUsers",
                table: "TestUsers");

            migrationBuilder.DropIndex(
                name: "IX_TestUsers_TestId",
                table: "TestUsers");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_TestUserId",
                table: "TestAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TestUsers",
                type: "character varying(26)",
                maxLength: 26,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "TestUserId",
                table: "TestAnswers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)");

            migrationBuilder.AddColumn<string>(
                name: "TestUserTestId",
                table: "TestAnswers",
                type: "character varying(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestUserUserId",
                table: "TestAnswers",
                type: "character varying(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestUsers",
                table: "TestUsers",
                columns: new[] { "TestId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestUserTestId_TestUserUserId",
                table: "TestAnswers",
                columns: new[] { "TestUserTestId", "TestUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_TestUsers_TestUserTestId_TestUserUserId",
                table: "TestAnswers",
                columns: new[] { "TestUserTestId", "TestUserUserId" },
                principalTable: "TestUsers",
                principalColumns: new[] { "TestId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
