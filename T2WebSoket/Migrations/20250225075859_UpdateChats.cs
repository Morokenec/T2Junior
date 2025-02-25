using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2WebSoket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatTypes_ChatTypeId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ChatTypeId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatTypeId",
                table: "Chats");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_TypeId",
                table: "Chats",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatTypes_TypeId",
                table: "Chats",
                column: "TypeId",
                principalTable: "ChatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatTypes_TypeId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_TypeId",
                table: "Chats");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatTypeId",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatTypeId",
                table: "Chats",
                column: "ChatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatTypes_ChatTypeId",
                table: "Chats",
                column: "ChatTypeId",
                principalTable: "ChatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
