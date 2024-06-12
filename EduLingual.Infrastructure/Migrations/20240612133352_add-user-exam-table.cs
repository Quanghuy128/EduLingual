using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduserexamtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exam",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    center_id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "edl",
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_user_center_id",
                        column: x => x.center_id,
                        principalSchema: "edl",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    point = table.Column<double>(type: "double precision", nullable: false),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.id);
                    table.ForeignKey(
                        name: "FK_question_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "edl",
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_user_exam_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "edl",
                        principalTable: "exam",
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

            migrationBuilder.CreateTable(
                name: "answer",
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
                    table.PrimaryKey("PK_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_answer_question_question_id",
                        column: x => x.question_id,
                        principalSchema: "edl",
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                schema: "edl",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_answer_question_id",
                schema: "edl",
                table: "answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_center_id",
                schema: "edl",
                table: "exam",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_course_id",
                schema: "edl",
                table: "exam",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_exam_id",
                schema: "edl",
                table: "question",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_exam_user_id",
                schema: "edl",
                table: "user_exam",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "user_exam",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "question",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "exam",
                schema: "edl");

            migrationBuilder.DropIndex(
                name: "IX_user_username",
                schema: "edl",
                table: "user");
        }
    }
}
