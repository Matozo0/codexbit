using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodexBit.Migrations
{
    /// <inheritdoc />
    public partial class UserModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Posts_PostsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PostsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostsId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostsId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PostsId",
                table: "AspNetUsers",
                column: "PostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Posts_PostsId",
                table: "AspNetUsers",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
