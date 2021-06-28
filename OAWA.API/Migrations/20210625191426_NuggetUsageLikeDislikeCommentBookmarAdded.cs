using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OAWA.API.Migrations
{
    public partial class NuggetUsageLikeDislikeCommentBookmarAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NuggetBookmarks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NuggetId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreateddDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuggetBookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuggetBookmarks_Nuggets_NuggetId",
                        column: x => x.NuggetId,
                        principalTable: "Nuggets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NuggetBookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuggetComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NuggetId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreateddDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuggetComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuggetComments_Nuggets_NuggetId",
                        column: x => x.NuggetId,
                        principalTable: "Nuggets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NuggetComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuggetLikeDislikes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NuggetId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    LikeDislikeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuggetLikeDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuggetLikeDislikes_Nuggets_NuggetId",
                        column: x => x.NuggetId,
                        principalTable: "Nuggets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NuggetLikeDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuggetUsages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NuggetId = table.Column<long>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    StopTime = table.Column<DateTime>(nullable: false),
                    ElapsedTime = table.Column<int>(nullable: false),
                    SlideNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuggetUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuggetUsages_Nuggets_NuggetId",
                        column: x => x.NuggetId,
                        principalTable: "Nuggets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NuggetBookmarks_NuggetId",
                table: "NuggetBookmarks",
                column: "NuggetId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetBookmarks_UserId",
                table: "NuggetBookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetComments_NuggetId",
                table: "NuggetComments",
                column: "NuggetId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetComments_UserId",
                table: "NuggetComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetLikeDislikes_NuggetId",
                table: "NuggetLikeDislikes",
                column: "NuggetId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetLikeDislikes_UserId",
                table: "NuggetLikeDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NuggetUsages_NuggetId",
                table: "NuggetUsages",
                column: "NuggetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NuggetBookmarks");

            migrationBuilder.DropTable(
                name: "NuggetComments");

            migrationBuilder.DropTable(
                name: "NuggetLikeDislikes");

            migrationBuilder.DropTable(
                name: "NuggetUsages");
        }
    }
}
