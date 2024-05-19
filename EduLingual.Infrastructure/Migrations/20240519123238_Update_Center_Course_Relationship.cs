using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Center_Course_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "center_id",
                schema: "edl",
                table: "course",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_course_center_id",
                schema: "edl",
                table: "course",
                column: "center_id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_user_center_id",
                schema: "edl",
                table: "course",
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
                name: "FK_course_user_center_id",
                schema: "edl",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_course_center_id",
                schema: "edl",
                table: "course");

            migrationBuilder.DropColumn(
                name: "center_id",
                schema: "edl",
                table: "course");
        }
    }
}
