using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitiativeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitiativeStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Initiatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdStatus = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdeaName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Target = table.Column<string>(type: "TEXT", nullable: false),
                    Tasks = table.Column<string>(type: "TEXT", nullable: false),
                    Budget = table.Column<string>(type: "TEXT", nullable: false),
                    Relevance = table.Column<string>(type: "TEXT", nullable: false),
                    ExpectedResult = table.Column<string>(type: "TEXT", nullable: false),
                    StatusId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Initiatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Initiatives_InitiativeStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "InitiativeStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitiativeComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdInitiative = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InitiativeComments_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaInitiatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdInitiative = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MediafileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaInitiatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaInitiatives_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaInitiatives_Mediafiles_MediafileId",
                        column: x => x.MediafileId,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInitiatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdInitiative = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInitiatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInitiatives_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInitiatives_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdInitiative = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    InitiativeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Initiatives_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_InitiativeId",
                table: "InitiativeComments",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeComments_UserId",
                table: "InitiativeComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Initiatives_StatusId",
                table: "Initiatives",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_InitiativeId",
                table: "MediaInitiatives",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaInitiatives_MediafileId",
                table: "MediaInitiatives",
                column: "MediafileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_InitiativeId",
                table: "UserInitiatives",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInitiatives_UserId",
                table: "UserInitiatives",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_InitiativeId",
                table: "Votes",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitiativeComments");

            migrationBuilder.DropTable(
                name: "MediaInitiatives");

            migrationBuilder.DropTable(
                name: "UserInitiatives");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Initiatives");

            migrationBuilder.DropTable(
                name: "InitiativeStatuses");
        }
    }
}
