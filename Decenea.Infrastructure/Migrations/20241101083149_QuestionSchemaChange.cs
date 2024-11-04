using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuestionSchemaChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SecondsToAnswer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Answers",
                newName: "TestUserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "TestUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TestUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TestUsers",
                type: "character varying(26)",
                maxLength: 26,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "TestUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TestQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondsToAnswer",
                table: "TestQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "TestQuestions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Answers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Answers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                columns: new[] { "TestUserId", "QuestionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TestUsers");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "TestUsers");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "SecondsToAnswer",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TestQuestions");

            migrationBuilder.RenameColumn(
                name: "TestUserId",
                table: "Answers",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "TestUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TestUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondsToAnswer",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Questions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Answers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Answers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Answers",
                type: "character varying(26)",
                maxLength: 26,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestId",
                table: "Answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");
        }
    }
}
