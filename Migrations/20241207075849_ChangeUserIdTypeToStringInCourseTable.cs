using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SokanAcademy.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdTypeToStringInCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
