using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2JuniorAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserAvatarMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAvatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdUser = table.Column<Guid>(nullable: false),
                    IdMedia = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvatars_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAvatars_Mediafiles_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Mediafiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_IdUser",
                table: "UserAvatars",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_IdMedia",
                table: "UserAvatars",
                column: "IdMedia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAvatars");
        }
    }
}
