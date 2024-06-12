using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_id_created_at_on_exam_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_exam",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "edl",
                table: "user_exam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "edl",
                table: "user_exam",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_exam",
                schema: "edl",
                table: "user_exam",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_exam_exam_id",
                schema: "edl",
                table: "user_exam",
                column: "exam_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_exam",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.DropIndex(
                name: "IX_user_exam_exam_id",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "edl",
                table: "user_exam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_exam",
                schema: "edl",
                table: "user_exam",
                columns: new[] { "exam_id", "user_id" });
        }
    }
}
