using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_fields_usercourse_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "edl",
                table: "user_course",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "finished_at",
                schema: "edl",
                table: "user_course",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "joined_at",
                schema: "edl",
                table: "user_course",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "status",
                schema: "edl",
                table: "user_course",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_course_course_id",
                schema: "edl",
                table: "user_course",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropIndex(
                name: "IX_user_course_course_id",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "finished_at",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "joined_at",
                schema: "edl",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "edl",
                table: "user_course");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_course",
                schema: "edl",
                table: "user_course",
                columns: new[] { "course_id", "user_id" });
        }
    }
}
