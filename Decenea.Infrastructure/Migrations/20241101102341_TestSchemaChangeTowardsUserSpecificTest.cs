using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestSchemaChangeTowardsUserSpecificTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "TestUsers");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Tests");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TestUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TestGroups",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TestUsers");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TestGroups");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "TestUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingTime",
                table: "Tests",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
