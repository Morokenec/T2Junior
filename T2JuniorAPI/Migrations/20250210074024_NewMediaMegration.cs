using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMediaMegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.CreateTable(
                name: "MediaTypes",
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
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mediafiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdType = table.Column<Guid>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediafiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mediafiles_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mediafiles_MediaTypes_IdType",
                        column: x => x.IdType,
                        principalTable: "MediaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaClubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdClub = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaClubs", x => new { x.IdClub, x.IdMedia });
                    table.ForeignKey(
                        name: "FK_MediaClubs_Clubs_IdClub",
                        column: x => x.IdClub,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaClubs_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdComment = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCommentNavigationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaComments_Comments_IdCommentNavigationId",
                        column: x => x.IdCommentNavigationId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaComments_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdEvent = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaEvents", x => new { x.IdEvent, x.IdMedia });
                    table.ForeignKey(
                        name: "FK_MediaEvents_Events_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaEvents_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdNote = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMedia = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaNotes", x => new { x.IdNote, x.IdMedia });
                    table.ForeignKey(
                        name: "FK_MediaNotes_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaNotes_Notes_IdNote",
                        column: x => x.IdNote,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdAchievement = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => new { x.IdUser, x.IdAchievement });
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_IdAchievement",
                        column: x => x.IdAchievement,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAchievements_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_IdMedia",
                table: "Achievements",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_MediaClubs_IdMedia",
                table: "MediaClubs",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_MediaComments_IdCommentNavigationId",
                table: "MediaComments",
                column: "IdCommentNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaComments_IdMedia",
                table: "MediaComments",
                column: "IdMedia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaEvents_IdMedia",
                table: "MediaEvents",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_Mediafiles_IdType",
                table: "Mediafiles",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Mediafiles_IdUser",
                table: "Mediafiles",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotes_IdMedia",
                table: "MediaNotes",
                column: "IdMedia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_IdAchievement",
                table: "UserAchievements",
                column: "IdAchievement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaClubs");

            migrationBuilder.DropTable(
                name: "MediaComments");

            migrationBuilder.DropTable(
                name: "MediaEvents");

            migrationBuilder.DropTable(
                name: "MediaNotes");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Mediafiles");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");
        }
    }
}
