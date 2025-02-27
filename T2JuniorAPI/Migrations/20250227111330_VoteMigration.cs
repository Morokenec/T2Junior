using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class VoteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Initiatives_InitiativeId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_InitiativeId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "InitiativeId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Votes");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IdInitiative",
                table: "Votes",
                column: "IdInitiative");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IdUser",
                table: "Votes",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_IdUser",
                table: "Votes",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Initiatives_IdInitiative",
                table: "Votes",
                column: "IdInitiative",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_IdUser",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Initiatives_IdInitiative",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_IdInitiative",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_IdUser",
                table: "Votes");

            migrationBuilder.AddColumn<Guid>(
                name: "InitiativeId",
                table: "Votes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Votes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Votes_InitiativeId",
                table: "Votes",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Initiatives_InitiativeId",
                table: "Votes",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
