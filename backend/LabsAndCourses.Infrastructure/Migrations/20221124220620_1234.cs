using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabsAndCoursesManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Didactics",
                table: "Didactics");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Didactics",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Didactics",
                table: "Didactics",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Didactics",
                table: "Didactics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Didactics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Didactics",
                table: "Didactics",
                columns: new[] { "CourseId", "TeacherId" });
        }
    }
}
