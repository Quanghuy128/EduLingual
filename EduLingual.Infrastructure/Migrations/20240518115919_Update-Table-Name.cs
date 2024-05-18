using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedback_Courses_course_id",
                schema: "edl",
                table: "CourseFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedback_feedback_feedback_id",
                schema: "edl",
                table: "CourseFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseAreas_area_id",
                schema: "edl",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesCategories_category_id",
                schema: "edl",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesLanguages_language_id",
                schema: "edl",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_Courses_course_id",
                schema: "edl",
                table: "UserCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_user_user_id",
                schema: "edl",
                table: "UserCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourse",
                schema: "edl",
                table: "UserCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesLanguages",
                schema: "edl",
                table: "CoursesLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesCategories",
                schema: "edl",
                table: "CoursesCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "edl",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFeedback",
                schema: "edl",
                table: "CourseFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAreas",
                schema: "edl",
                table: "CourseAreas");

            migrationBuilder.RenameTable(
                name: "UserCourse",
                schema: "edl",
                newName: "user_course",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "CoursesLanguages",
                schema: "edl",
                newName: "course_language",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "CoursesCategories",
                schema: "edl",
                newName: "course_category",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "edl",
                newName: "course",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "CourseFeedback",
                schema: "edl",
                newName: "course_feedback",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "CourseAreas",
                schema: "edl",
                newName: "course_area",
                newSchema: "edl");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourse_user_id",
                schema: "edl",
                table: "user_course",
                newName: "IX_user_course_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_language_id",
                schema: "edl",
                table: "course",
                newName: "IX_course_language_id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_category_id",
                schema: "edl",
                table: "course",
                newName: "IX_course_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_area_id",
                schema: "edl",
                table: "course",
                newName: "IX_course_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFeedback_feedback_id",
                schema: "edl",
                table: "course_feedback",
                newName: "IX_course_feedback_feedback_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course",
                columns: new[] { "course_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_course_language",
                schema: "edl",
                table: "course_language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_course_category",
                schema: "edl",
                table: "course_category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_course",
                schema: "edl",
                table: "course",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_course_feedback",
                schema: "edl",
                table: "course_feedback",
                columns: new[] { "course_id", "feedback_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_course_area",
                schema: "edl",
                table: "course_area",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_course_area_area_id",
                schema: "edl",
                table: "course",
                column: "area_id",
                principalSchema: "edl",
                principalTable: "course_area",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_course_category_category_id",
                schema: "edl",
                table: "course",
                column: "category_id",
                principalSchema: "edl",
                principalTable: "course_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_course_language_language_id",
                schema: "edl",
                table: "course",
                column: "language_id",
                principalSchema: "edl",
                principalTable: "course_language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_feedback_course_course_id",
                schema: "edl",
                table: "course_feedback",
                column: "course_id",
                principalSchema: "edl",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_feedback_feedback_feedback_id",
                schema: "edl",
                table: "course_feedback",
                column: "feedback_id",
                principalSchema: "edl",
                principalTable: "feedback",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_course_course_course_id",
                schema: "edl",
                table: "user_course",
                column: "course_id",
                principalSchema: "edl",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_course_user_user_id",
                schema: "edl",
                table: "user_course",
                column: "user_id",
                principalSchema: "edl",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_course_area_area_id",
                schema: "edl",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_course_course_category_category_id",
                schema: "edl",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_course_course_language_language_id",
                schema: "edl",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_course_feedback_course_course_id",
                schema: "edl",
                table: "course_feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_course_feedback_feedback_feedback_id",
                schema: "edl",
                table: "course_feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_user_course_course_course_id",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropForeignKey(
                name: "FK_user_course_user_user_id",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course_language",
                schema: "edl",
                table: "course_language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course_feedback",
                schema: "edl",
                table: "course_feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course_category",
                schema: "edl",
                table: "course_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course_area",
                schema: "edl",
                table: "course_area");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course",
                schema: "edl",
                table: "course");

            migrationBuilder.RenameTable(
                name: "user_course",
                schema: "edl",
                newName: "UserCourse",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "course_language",
                schema: "edl",
                newName: "CoursesLanguages",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "course_feedback",
                schema: "edl",
                newName: "CourseFeedback",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "course_category",
                schema: "edl",
                newName: "CoursesCategories",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "course_area",
                schema: "edl",
                newName: "CourseAreas",
                newSchema: "edl");

            migrationBuilder.RenameTable(
                name: "course",
                schema: "edl",
                newName: "Courses",
                newSchema: "edl");

            migrationBuilder.RenameIndex(
                name: "IX_user_course_user_id",
                schema: "edl",
                table: "UserCourse",
                newName: "IX_UserCourse_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_feedback_feedback_id",
                schema: "edl",
                table: "CourseFeedback",
                newName: "IX_CourseFeedback_feedback_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_language_id",
                schema: "edl",
                table: "Courses",
                newName: "IX_Courses_language_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_category_id",
                schema: "edl",
                table: "Courses",
                newName: "IX_Courses_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_area_id",
                schema: "edl",
                table: "Courses",
                newName: "IX_Courses_area_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourse",
                schema: "edl",
                table: "UserCourse",
                columns: new[] { "course_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesLanguages",
                schema: "edl",
                table: "CoursesLanguages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFeedback",
                schema: "edl",
                table: "CourseFeedback",
                columns: new[] { "course_id", "feedback_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesCategories",
                schema: "edl",
                table: "CoursesCategories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAreas",
                schema: "edl",
                table: "CourseAreas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "edl",
                table: "Courses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedback_Courses_course_id",
                schema: "edl",
                table: "CourseFeedback",
                column: "course_id",
                principalSchema: "edl",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedback_feedback_feedback_id",
                schema: "edl",
                table: "CourseFeedback",
                column: "feedback_id",
                principalSchema: "edl",
                principalTable: "feedback",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseAreas_area_id",
                schema: "edl",
                table: "Courses",
                column: "area_id",
                principalSchema: "edl",
                principalTable: "CourseAreas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesCategories_category_id",
                schema: "edl",
                table: "Courses",
                column: "category_id",
                principalSchema: "edl",
                principalTable: "CoursesCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesLanguages_language_id",
                schema: "edl",
                table: "Courses",
                column: "language_id",
                principalSchema: "edl",
                principalTable: "CoursesLanguages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_Courses_course_id",
                schema: "edl",
                table: "UserCourse",
                column: "course_id",
                principalSchema: "edl",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_user_user_id",
                schema: "edl",
                table: "UserCourse",
                column: "user_id",
                principalSchema: "edl",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
