using Microsoft.EntityFrameworkCore.Migrations;

namespace OAWA.API.Migrations
{
    public partial class AssignmentModelCanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxScore",
                table: "Assignments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "Assignments");
        }
    }
}
