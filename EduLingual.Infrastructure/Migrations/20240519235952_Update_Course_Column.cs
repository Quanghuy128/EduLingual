using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Course_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "language_id",
                schema: "edl",
                table: "course_category",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "edl",
                table: "course",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "tuition_fee",
                schema: "edl",
                table: "course",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_course_category_language_id",
                schema: "edl",
                table: "course_category",
                column: "language_id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_category_course_language_language_id",
                schema: "edl",
                table: "course_category",
                column: "language_id",
                principalSchema: "edl",
                principalTable: "course_language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_category_course_language_language_id",
                schema: "edl",
                table: "course_category");

            migrationBuilder.DropIndex(
                name: "IX_course_category_language_id",
                schema: "edl",
                table: "course_category");

            migrationBuilder.DropColumn(
                name: "language_id",
                schema: "edl",
                table: "course_category");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "edl",
                table: "course");

            migrationBuilder.DropColumn(
                name: "tuition_fee",
                schema: "edl",
                table: "course");
        }
    }
}
