using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SokanAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId1",
                table: "Courses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId1",
                table: "Courses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Courses");
        }
    }
}
