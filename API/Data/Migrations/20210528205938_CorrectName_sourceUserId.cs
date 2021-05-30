using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class CorrectName_sourceUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Users_sourseUserId",
                table: "UserLikes");

            migrationBuilder.RenameColumn(
                name: "sourseUserId",
                table: "UserLikes",
                newName: "sourceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Users_sourceUserId",
                table: "UserLikes",
                column: "sourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Users_sourceUserId",
                table: "UserLikes");

            migrationBuilder.RenameColumn(
                name: "sourceUserId",
                table: "UserLikes",
                newName: "sourseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Users_sourseUserId",
                table: "UserLikes",
                column: "sourseUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
