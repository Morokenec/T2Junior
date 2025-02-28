using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitiativeNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeComments_AspNetUsers_UserId",
                table: "InitiativeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeComments_Initiatives_InitiativeId",
                table: "InitiativeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_StatusId",
                table: "Initiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaInitiatives_Initiatives_InitiativeId",
                table: "MediaInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaInitiatives_Mediafiles_MediafileId",
                table: "MediaInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_AspNetUsers_UserId",
                table: "UserInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_Initiatives_InitiativeId",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_UserInitiatives_InitiativeId",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_UserInitiatives_UserId",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_MediaInitiatives_InitiativeId",
                table: "MediaInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_MediaInitiatives_MediafileId",
                table: "MediaInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_Initiatives_StatusId",
                table: "Initiatives");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeComments_InitiativeId",
                table: "InitiativeComments");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeComments_UserId",
                table: "InitiativeComments");

            migrationBuilder.DropColumn(
                name: "InitiativeId",
                table: "UserInitiatives");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInitiatives");

            migrationBuilder.DropColumn(
                name: "InitiativeId",
                table: "MediaInitiatives");

            migrationBuilder.DropColumn(
                name: "MediafileId",
                table: "MediaInitiatives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Initiatives");

            migrationBuilder.DropColumn(
                name: "InitiativeId",
                table: "InitiativeComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InitiativeComments");

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_IdInitiative",
                table: "UserInitiatives",
                column: "IdInitiative");

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_IdUser",
                table: "UserInitiatives",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_IdInitiative",
                table: "MediaInitiatives",
                column: "IdInitiative");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_IdMedia",
                table: "MediaInitiatives",
                column: "IdMedia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_IdStatus",
                table: "Initiatives",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_IdInitiative",
                table: "InitiativeComments",
                column: "IdInitiative");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_IdUser",
                table: "InitiativeComments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeComments_AspNetUsers_IdUser",
                table: "InitiativeComments",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeComments_Initiatives_IdInitiative",
                table: "InitiativeComments",
                column: "IdInitiative",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_IdStatus",
                table: "Initiatives",
                column: "IdStatus",
                principalTable: "InitiativeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaInitiatives_Initiatives_IdInitiative",
                table: "MediaInitiatives",
                column: "IdInitiative",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaInitiatives_Mediafiles_IdMedia",
                table: "MediaInitiatives",
                column: "IdMedia",
                principalTable: "Mediafiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_AspNetUsers_IdUser",
                table: "UserInitiatives",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_Initiatives_IdInitiative",
                table: "UserInitiatives",
                column: "IdInitiative",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeComments_AspNetUsers_IdUser",
                table: "InitiativeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeComments_Initiatives_IdInitiative",
                table: "InitiativeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_IdStatus",
                table: "Initiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaInitiatives_Initiatives_IdInitiative",
                table: "MediaInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaInitiatives_Mediafiles_IdMedia",
                table: "MediaInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_AspNetUsers_IdUser",
                table: "UserInitiatives");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInitiatives_Initiatives_IdInitiative",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_UserInitiatives_IdInitiative",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_UserInitiatives_IdUser",
                table: "UserInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_MediaInitiatives_IdInitiative",
                table: "MediaInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_MediaInitiatives_IdMedia",
                table: "MediaInitiatives");

            migrationBuilder.DropIndex(
                name: "IX_Initiatives_IdStatus",
                table: "Initiatives");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeComments_IdInitiative",
                table: "InitiativeComments");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeComments_IdUser",
                table: "InitiativeComments");

            migrationBuilder.AddColumn<Guid>(
                name: "InitiativeId",
                table: "UserInitiatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserInitiatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InitiativeId",
                table: "MediaInitiatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MediafileId",
                table: "MediaInitiatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Initiatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InitiativeId",
                table: "InitiativeComments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "InitiativeComments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_InitiativeId",
                table: "UserInitiatives",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_UserId",
                table: "UserInitiatives",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_InitiativeId",
                table: "MediaInitiatives",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_MediafileId",
                table: "MediaInitiatives",
                column: "MediafileId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_StatusId",
                table: "Initiatives",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_InitiativeId",
                table: "InitiativeComments",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_UserId",
                table: "InitiativeComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeComments_AspNetUsers_UserId",
                table: "InitiativeComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeComments_Initiatives_InitiativeId",
                table: "InitiativeComments",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Initiatives_InitiativeStatuses_StatusId",
                table: "Initiatives",
                column: "StatusId",
                principalTable: "InitiativeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaInitiatives_Initiatives_InitiativeId",
                table: "MediaInitiatives",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaInitiatives_Mediafiles_MediafileId",
                table: "MediaInitiatives",
                column: "MediafileId",
                principalTable: "Mediafiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_AspNetUsers_UserId",
                table: "UserInitiatives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInitiatives_Initiatives_InitiativeId",
                table: "UserInitiatives",
                column: "InitiativeId",
                principalTable: "Initiatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
