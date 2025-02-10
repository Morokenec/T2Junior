using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class MediaMegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_AspNetUsers_IdUser",
                table: "ClubUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_ClubRoles_IdRole",
                table: "ClubUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_Clubs_IdClub",
                table: "ClubUsers");

            migrationBuilder.DropColumn(
                name: "CreationDatetime",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "FactParticpants",
                table: "Events",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_AspNetUsers_IdUser",
                table: "ClubUsers",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_ClubRoles_IdRole",
                table: "ClubUsers",
                column: "IdRole",
                principalTable: "ClubRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_Clubs_IdClub",
                table: "ClubUsers",
                column: "IdClub",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_AspNetUsers_IdUser",
                table: "ClubUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_ClubRoles_IdRole",
                table: "ClubUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubUsers_Clubs_IdClub",
                table: "ClubUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FactParticpants",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDatetime",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_AspNetUsers_IdUser",
                table: "ClubUsers",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_ClubRoles_IdRole",
                table: "ClubUsers",
                column: "IdRole",
                principalTable: "ClubRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubUsers_Clubs_IdClub",
                table: "ClubUsers",
                column: "IdClub",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
