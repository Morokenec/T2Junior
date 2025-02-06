using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_IdClubNavigationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventDirections_IdDirectionNavigationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_IdClubNavigationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_IdDirectionNavigationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IdClubNavigationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IdDirectionNavigationId",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClubId",
                table: "Events",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdClub",
                table: "Events",
                column: "IdClub");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdDirection",
                table: "Events",
                column: "IdDirection");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_IdClub",
                table: "Events",
                column: "IdClub",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventDirections_IdDirection",
                table: "Events",
                column: "IdDirection",
                principalTable: "EventDirections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_IdClub",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventDirections_IdDirection",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ClubId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_IdClub",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_IdDirection",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "IdClubNavigationId",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdDirectionNavigationId",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdClubNavigationId",
                table: "Events",
                column: "IdClubNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdDirectionNavigationId",
                table: "Events",
                column: "IdDirectionNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_IdClubNavigationId",
                table: "Events",
                column: "IdClubNavigationId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventDirections_IdDirectionNavigationId",
                table: "Events",
                column: "IdDirectionNavigationId",
                principalTable: "EventDirections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
