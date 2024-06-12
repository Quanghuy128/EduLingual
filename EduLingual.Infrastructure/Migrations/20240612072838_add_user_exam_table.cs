using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_exam_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_course_exam_id",
                schema: "edl",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_user_creator_id",
                schema: "edl",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "exam_id",
                schema: "edl",
                table: "Exams",
                newName: "course_id");

            migrationBuilder.RenameColumn(
                name: "creator_id",
                schema: "edl",
                table: "Exams",
                newName: "center_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_exam_id",
                schema: "edl",
                table: "Exams",
                newName: "IX_Exams_course_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_creator_id",
                schema: "edl",
                table: "Exams",
                newName: "IX_Exams_center_id");

            migrationBuilder.AddColumn<double>(
                name: "point",
                schema: "edl",
                table: "Questions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                schema: "edl",
                table: "Exams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "user_exam",
                schema: "edl",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_exam", x => new { x.exam_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_user_exam_Exams_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "edl",
                        principalTable: "Exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_exam_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "edl",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_exam_user_id",
                schema: "edl",
                table: "user_exam",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_course_course_id",
                schema: "edl",
                table: "Exams",
                column: "course_id",
                principalSchema: "edl",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_user_center_id",
                schema: "edl",
                table: "Exams",
                column: "center_id",
                principalSchema: "edl",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_course_course_id",
                schema: "edl",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_user_center_id",
                schema: "edl",
                table: "Exams");

            migrationBuilder.DropTable(
                name: "user_exam",
                schema: "edl");

            migrationBuilder.DropColumn(
                name: "point",
                schema: "edl",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "title",
                schema: "edl",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "course_id",
                schema: "edl",
                table: "Exams",
                newName: "exam_id");

            migrationBuilder.RenameColumn(
                name: "center_id",
                schema: "edl",
                table: "Exams",
                newName: "creator_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_course_id",
                schema: "edl",
                table: "Exams",
                newName: "IX_Exams_exam_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_center_id",
                schema: "edl",
                table: "Exams",
                newName: "IX_Exams_creator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_course_exam_id",
                schema: "edl",
                table: "Exams",
                column: "exam_id",
                principalSchema: "edl",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_user_creator_id",
                schema: "edl",
                table: "Exams",
                column: "creator_id",
                principalSchema: "edl",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
