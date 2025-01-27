using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccumulatedPoints",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClubRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rules = table.Column<string>(type: "TEXT", nullable: false),
                    Target = table.Column<string>(type: "TEXT", nullable: false),
                    Raiting = table.Column<int>(type: "INTEGER", nullable: false),
                    Reports = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDirections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscribers",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "TEXT", nullable: false),
                    IdSubscriber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscribers", x => new { x.IdUser, x.IdSubscriber });
                    table.ForeignKey(
                        name: "FK_UserSubscribers_AspNetUsers_IdSubscriber",
                        column: x => x.IdSubscriber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscribers_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WallTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClubUsers",
                columns: table => new
                {
                    IdClub = table.Column<string>(type: "TEXT", nullable: false),
                    IdUser = table.Column<string>(type: "TEXT", nullable: false),
                    IdRole = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubUsers", x => new { x.IdClub, x.IdUser, x.IdRole });
                    table.ForeignKey(
                        name: "FK_ClubUsers_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubUsers_ClubRoles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "ClubRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubUsers_Clubs_IdClub",
                        column: x => x.IdClub,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdClub = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDirection = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Place = table.Column<string>(type: "TEXT", nullable: false),
                    Datetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberParticpants = table.Column<int>(type: "INTEGER", nullable: false),
                    FactParticpants = table.Column<int>(type: "INTEGER", nullable: false),
                    Raiting = table.Column<int>(type: "INTEGER", nullable: false),
                    IdClubNavigationId = table.Column<string>(type: "TEXT", nullable: false),
                    IdDirectionNavigationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Clubs_IdClubNavigationId",
                        column: x => x.IdClubNavigationId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_EventDirections_IdDirectionNavigationId",
                        column: x => x.IdDirectionNavigationId,
                        principalTable: "EventDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdType = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOwner = table.Column<string>(type: "TEXT", nullable: false),
                    IdTypeNavigationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walls_AspNetUsers_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Walls_Clubs_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Walls_WallTypes_IdTypeNavigationId",
                        column: x => x.IdTypeNavigationId,
                        principalTable: "WallTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdWall = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdRepost = table.Column<int>(type: "INTEGER", nullable: true),
                    LikeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStatusNavigationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdWallNavigationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_NoteStatuses_IdStatusNavigationId",
                        column: x => x.IdStatusNavigationId,
                        principalTable: "NoteStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Notes_IdRepost",
                        column: x => x.IdRepost,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_Walls_IdWallNavigationId",
                        column: x => x.IdWallNavigationId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdNote = table.Column<string>(type: "TEXT", nullable: false),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    ParrentCommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    LikeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IdNoteNavigationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUserNavigationId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_IdUserNavigationId",
                        column: x => x.IdUserNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParrentCommentId",
                        column: x => x.ParrentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Notes_IdNoteNavigationId",
                        column: x => x.IdNoteNavigationId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubUsers_IdRole",
                table: "ClubUsers",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUsers_IdUser",
                table: "ClubUsers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdNoteNavigationId",
                table: "Comments",
                column: "IdNoteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdUserNavigationId",
                table: "Comments",
                column: "IdUserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParrentCommentId",
                table: "Comments",
                column: "ParrentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdClubNavigationId",
                table: "Events",
                column: "IdClubNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdDirectionNavigationId",
                table: "Events",
                column: "IdDirectionNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdRepost",
                table: "Notes",
                column: "IdRepost");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdStatusNavigationId",
                table: "Notes",
                column: "IdStatusNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdWallNavigationId",
                table: "Notes",
                column: "IdWallNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_IdSubscriber",
                table: "UserSubscribers",
                column: "IdSubscriber");

            migrationBuilder.CreateIndex(
                name: "IX_Walls_IdOwner",
                table: "Walls",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_Walls_IdTypeNavigationId",
                table: "Walls",
                column: "IdTypeNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubUsers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "UserSubscribers");

            migrationBuilder.DropTable(
                name: "ClubRoles");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "EventDirections");

            migrationBuilder.DropTable(
                name: "NoteStatuses");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "WallTypes");

            migrationBuilder.DropColumn(
                name: "AccumulatedPoints",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "AspNetUsers");
        }
    }
}
