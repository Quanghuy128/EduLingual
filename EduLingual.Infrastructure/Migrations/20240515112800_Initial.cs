using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "edl");

            migrationBuilder.CreateTable(
                name: "CourseAreas",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAreas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CoursesCategories",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CoursesLanguages",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesLanguages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    duration = table.Column<string>(type: "text", nullable: false),
                    area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseAreas_area_id",
                        column: x => x.area_id,
                        principalSchema: "edl",
                        principalTable: "CourseAreas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CoursesCategories_category_id",
                        column: x => x.category_id,
                        principalSchema: "edl",
                        principalTable: "CoursesCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CoursesLanguages_language_id",
                        column: x => x.language_id,
                        principalSchema: "edl",
                        principalTable: "CoursesLanguages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "edl",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                schema: "edl",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_feedback_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "edl",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourse",
                schema: "edl",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => new { x.course_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_UserCourse_Courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "edl",
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourse_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "edl",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFeedback",
                schema: "edl",
                columns: table => new
                {
                    course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    feedback_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeedback", x => new { x.course_id, x.feedback_id });
                    table.ForeignKey(
                        name: "FK_CourseFeedback_Courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "edl",
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFeedback_feedback_feedback_id",
                        column: x => x.feedback_id,
                        principalSchema: "edl",
                        principalTable: "feedback",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedback_feedback_id",
                schema: "edl",
                table: "CourseFeedback",
                column: "feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_area_id",
                schema: "edl",
                table: "Courses",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_category_id",
                schema: "edl",
                table: "Courses",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_language_id",
                schema: "edl",
                table: "Courses",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_user_id",
                schema: "edl",
                table: "feedback",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                schema: "edl",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_user_id",
                schema: "edl",
                table: "UserCourse",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseFeedback",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "UserCourse",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "feedback",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "user",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "CourseAreas",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "CoursesCategories",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "CoursesLanguages",
                schema: "edl");

            migrationBuilder.DropTable(
                name: "role",
                schema: "edl");
        }
    }
}
