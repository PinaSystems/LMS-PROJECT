using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OAWA.API.Migrations
{
    public partial class AssessmentModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    AttachmentFile = table.Column<string>(nullable: true),
                    NuggetId = table.Column<long>(nullable: false),
                    LessonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentSubmissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<long>(nullable: false),
                    StudentId = table.Column<long>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TrainerRemarks = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    SubmissionStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentSubmissions_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSubmissions_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSubmissions_AssessmentId",
                table: "AssessmentSubmissions",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSubmissions_StudentId",
                table: "AssessmentSubmissions",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSubmissions");

            migrationBuilder.DropTable(
                name: "Assessments");
        }
    }
}
