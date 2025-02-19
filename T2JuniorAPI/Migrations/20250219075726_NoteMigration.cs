using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class NoteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteStatuses_IdStatusNavigationId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Walls_IdWallNavigationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdStatusNavigationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdWallNavigationId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IdStatusNavigationId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IdWallNavigationId",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdStatus",
                table: "Notes",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdWall",
                table: "Notes",
                column: "IdWall");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteStatuses_IdStatus",
                table: "Notes",
                column: "IdStatus",
                principalTable: "NoteStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Walls_IdWall",
                table: "Notes",
                column: "IdWall",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteStatuses_IdStatus",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Walls_IdWall",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdStatus",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdWall",
                table: "Notes");

            migrationBuilder.AddColumn<Guid>(
                name: "IdStatusNavigationId",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdWallNavigationId",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdStatusNavigationId",
                table: "Notes",
                column: "IdStatusNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdWallNavigationId",
                table: "Notes",
                column: "IdWallNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteStatuses_IdStatusNavigationId",
                table: "Notes",
                column: "IdStatusNavigationId",
                principalTable: "NoteStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Walls_IdWallNavigationId",
                table: "Notes",
                column: "IdWallNavigationId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
