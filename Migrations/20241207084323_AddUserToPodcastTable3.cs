using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SokanAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToPodcastTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Podcasts_AspNetUsers_userId",
                table: "Podcasts");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Podcasts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Podcasts_userId",
                table: "Podcasts",
                newName: "IX_Podcasts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Podcasts_AspNetUsers_UserId",
                table: "Podcasts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Podcasts_AspNetUsers_UserId",
                table: "Podcasts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Podcasts",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Podcasts_UserId",
                table: "Podcasts",
                newName: "IX_Podcasts_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Podcasts_AspNetUsers_userId",
                table: "Podcasts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
