using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalleryApp.Data.Migrations
{
    public partial class usertableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAppId",
                table: "Galleries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_UserAppId",
                table: "Galleries",
                column: "UserAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_AspNetUsers_UserAppId",
                table: "Galleries",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_AspNetUsers_UserAppId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_UserAppId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "UserAppId",
                table: "Galleries");
        }
    }
}
