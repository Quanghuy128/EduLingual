using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "total_question_right",
                schema: "edl",
                table: "user_exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_question_wrong",
                schema: "edl",
                table: "user_exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_question",
                schema: "edl",
                table: "exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_question_right",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.DropColumn(
                name: "total_question_wrong",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.DropColumn(
                name: "total_question",
                schema: "edl",
                table: "exam");
        }
    }
}
