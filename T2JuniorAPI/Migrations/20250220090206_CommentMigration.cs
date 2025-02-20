using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class CommentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_AspNetUsers_UserId",
                table: "CommentLike");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_Comments_CommentId",
                table: "CommentLike");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Notes_IdNoteNavigationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Notes_NoteId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdNoteNavigationId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLike",
                table: "CommentLike");

            migrationBuilder.DropColumn(
                name: "IdNoteNavigationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IdUserNavigationId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "NoteLikes");

            migrationBuilder.RenameTable(
                name: "CommentLike",
                newName: "CommentLikes");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "NoteLikes",
                newName: "IX_NoteLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_NoteId",
                table: "NoteLikes",
                newName: "IX_NoteLikes_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLike_UserId",
                table: "CommentLikes",
                newName: "IX_CommentLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLike_CommentId",
                table: "CommentLikes",
                newName: "IX_CommentLikes_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteLikes",
                table: "NoteLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdNote",
                table: "Comments",
                column: "IdNote");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdUser",
                table: "Comments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserId",
                table: "CommentLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Comments_CommentId",
                table: "CommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_IdUser",
                table: "Comments",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Notes_IdNote",
                table: "Comments",
                column: "IdNote",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLikes_AspNetUsers_UserId",
                table: "NoteLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLikes_Notes_NoteId",
                table: "NoteLikes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comments_CommentId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_IdUser",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Notes_IdNote",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLikes_AspNetUsers_UserId",
                table: "NoteLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLikes_Notes_NoteId",
                table: "NoteLikes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdNote",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdUser",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteLikes",
                table: "NoteLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes");

            migrationBuilder.RenameTable(
                name: "NoteLikes",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "CommentLikes",
                newName: "CommentLike");

            migrationBuilder.RenameIndex(
                name: "IX_NoteLikes_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteLikes_NoteId",
                table: "Likes",
                newName: "IX_Likes_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_UserId",
                table: "CommentLike",
                newName: "IX_CommentLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLike",
                newName: "IX_CommentLike_CommentId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdNoteNavigationId",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUserNavigationId",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLike",
                table: "CommentLike",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdNoteNavigationId",
                table: "Comments",
                column: "IdNoteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_AspNetUsers_UserId",
                table: "CommentLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_Comments_CommentId",
                table: "CommentLike",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Notes_IdNoteNavigationId",
                table: "Comments",
                column: "IdNoteNavigationId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Notes_NoteId",
                table: "Likes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
