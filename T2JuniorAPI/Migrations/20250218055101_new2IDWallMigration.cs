using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class new2IDWallMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walls_AspNetUsers_ApplicationUserId",
                table: "Walls");

            migrationBuilder.DropForeignKey(
                name: "FK_Walls_Clubs_ClubId",
                table: "Walls");

            migrationBuilder.DropColumn(
                name: "IdOwner",
                table: "Walls");

            migrationBuilder.RenameColumn(
                name: "ClubId",
                table: "Walls",
                newName: "IdUserOwner");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Walls",
                newName: "IdClubOwner");

            migrationBuilder.RenameIndex(
                name: "IX_Walls_ClubId",
                table: "Walls",
                newName: "IX_Walls_IdUserOwner");

            migrationBuilder.RenameIndex(
                name: "IX_Walls_ApplicationUserId",
                table: "Walls",
                newName: "IX_Walls_IdClubOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_AspNetUsers_IdUserOwner",
                table: "Walls",
                column: "IdUserOwner",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_Clubs_IdClubOwner",
                table: "Walls",
                column: "IdClubOwner",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walls_AspNetUsers_IdUserOwner",
                table: "Walls");

            migrationBuilder.DropForeignKey(
                name: "FK_Walls_Clubs_IdClubOwner",
                table: "Walls");

            migrationBuilder.RenameColumn(
                name: "IdUserOwner",
                table: "Walls",
                newName: "ClubId");

            migrationBuilder.RenameColumn(
                name: "IdClubOwner",
                table: "Walls",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Walls_IdUserOwner",
                table: "Walls",
                newName: "IX_Walls_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Walls_IdClubOwner",
                table: "Walls",
                newName: "IX_Walls_ApplicationUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdOwner",
                table: "Walls",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_AspNetUsers_ApplicationUserId",
                table: "Walls",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walls_Clubs_ClubId",
                table: "Walls",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }
    }
}
