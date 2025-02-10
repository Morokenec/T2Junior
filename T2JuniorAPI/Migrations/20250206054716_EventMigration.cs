using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Datetime",
                table: "Events",
                newName: "StartDatetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDatetime",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDatetime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StartDatetime",
                table: "Events",
                newName: "Datetime");
        }
    }
}
