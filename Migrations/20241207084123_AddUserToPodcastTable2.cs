using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SokanAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToPodcastTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Podcasts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Podcasts_userId",
                table: "Podcasts",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Podcasts_AspNetUsers_userId",
                table: "Podcasts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Podcasts_AspNetUsers_userId",
                table: "Podcasts");

            migrationBuilder.DropIndex(
                name: "IX_Podcasts_userId",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Podcasts");
        }
    }
}
