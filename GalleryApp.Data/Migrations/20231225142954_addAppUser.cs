using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalleryApp.Data.Migrations
{
    public partial class addAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Galleries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_AppUserId",
                table: "Galleries",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_AspNetUsers_AppUserId",
                table: "Galleries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_AspNetUsers_AppUserId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_AppUserId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
