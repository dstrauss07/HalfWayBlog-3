using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class UpdatedTagCategoryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_BlogPostId",
                table: "BlogTag");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "BlogTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "BlogTag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_BlogPostId",
                table: "BlogTag",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
