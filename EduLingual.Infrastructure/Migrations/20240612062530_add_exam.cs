using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_exam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.id);
                    table.ForeignKey(
                        name: "FK_Exams_course_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "edl",
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_user_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "edl",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "edl",
                        principalTable: "Exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    is_correct = table.Column<bool>(type: "boolean", nullable: false),
                    question_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_question_id",
                        column: x => x.question_id,
                        principalSchema: "edl",
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_question_id",
                schema: "edl",
                table: "Answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_creator_id",
                schema: "edl",
                table: "Exams",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_exam_id",
                schema: "edl",
                table: "Exams",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_exam_id",
                schema: "edl",
                table: "Questions",
                column: "exam_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "Exams",
                schema: "edl");
        }
    }
}
