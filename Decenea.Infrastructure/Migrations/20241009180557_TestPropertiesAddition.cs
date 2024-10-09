using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Decenea.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestPropertiesAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinutesToComplete",
                table: "Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingTime",
                table: "Tests",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesToComplete",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Tests");
        }
    }
}
